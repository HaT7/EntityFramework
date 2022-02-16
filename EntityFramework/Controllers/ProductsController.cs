using System.Collections.Generic;
using System.Linq;
using EntityFramework.Data;
using EntityFramework.Models;
using EntityFramework.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EntityFramework.Controllers
{
    public class ProductsController: Controller
    {
        //Mình nói với hệ thống cấp cho mình một cái db, thằng db mình tạo ở line 15 sẽ tiến hành
        //truyền vào cho cái thằng _db private ở line 13.
        private readonly ApplicationDbContext _db;

        public ProductsController(ApplicationDbContext db)
        {
            _db = db;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            var productList = _db.Products.Include(c => c.Category).ToList(); //Sủ dụng câu lệnh LinkQ để lấy hết dữ liệu từ Products lên
            return View(productList);
        }
        
        //Create
        [HttpGet]
        public IActionResult Create()
        {
            //ngoài Views sẽ là những object của Product nên mình sẽ truyền cho Models một cái product, thành ra
            //cái Product sẽ được tạo mới nên mình sẽ return vs một cái Views là new một cái Product.
            return View(new Product());
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        
        //Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _db.Products.Find(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            _db.Products.Update(product);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        
        //UpSert
        [NonAction]
        private IEnumerable<SelectListItem> CategorySelectListItems()
        //Ở đây chúng ta trả về CategoryList nên kiểu dữ liệu là IEnumerable<SelectListItem>
        {
            var categoryList = _db.Categories.ToList(); //Lấy toàn bộ category trong database ra
            //Lấy dữ liệu thằng categoryList ở trên, tạo ra một anonymous function, cứ mỗi thằng
            //category là sẽ tạo một SelectListItem, trong mỗi SelectListItem sẽ có Text và Value.
            var result = categoryList.Select(category => new SelectListItem
            {
                Text = category.Name,           //Cái này là hiển thị lên cho user thấy
                Value = category.Id.ToString()  //Cái này là gửi về hệ thống (gửi về một cái Id của category)
            });

            return result; //result này tương đương vs một item trong category list
        }

        [HttpGet]
        public IActionResult UpSert(int? id)
        {
            ProductUpSertViewModels productVm = new ProductUpSertViewModels();
            productVm.CategoryList = CategorySelectListItems();
            //Cho insert/ create
            if (id == 0 || id == null)
            {
                productVm.Product = new Product();
                return View(productVm);
            }
            
            //Cho update/ edit
            productVm.Product = _db.Products.Find(id);
            return View(productVm);
        }

        [HttpPost]
        public IActionResult UpSert(ProductUpSertViewModels productUpSertViewModels)
        {
            if (!ModelState.IsValid)
            {
                //Sẽ có trường hợp ko chọn cái category nào trong category list, nên cần phải nhập
                //lại dữ liệu của CategoryList vào
                productUpSertViewModels.CategoryList = CategorySelectListItems();
                return View(productUpSertViewModels);
            }
            
            if (productUpSertViewModels.Product.ProductId == 0 || productUpSertViewModels.Product.ProductId == null)
            {
                _db.Products.Add(productUpSertViewModels.Product);
            }
            else
            {
                _db.Products.Update(productUpSertViewModels.Product);
            }
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        
        //Delete
        [HttpGet]
        public IActionResult Delete(int productId) //nhận vào 1 cái productId có kiểu dữ liệu là int
        {
            var product = _db.Products.Find(productId); //Sau khi find xong cái productId sẽ truyền vào cái biến product
            _db.Products.Remove(product);               //Xóa cái product đó
            _db.SaveChanges();                          //Sau khi xóa sẽ lưu lại
            return RedirectToAction(nameof(Index));     //Cuối cùng trả về cái action là Index.
        }
    }
}