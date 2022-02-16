using System.Collections.Generic;
using System.Linq;
using EntityFramework.Data;
using EntityFramework.Models;
using EntityFramework.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.Controllers
{
    public class OdersController : Controller
    {
        private readonly ApplicationDbContext _db;

        public OdersController(ApplicationDbContext db)
        {
            _db = db;
        }
        
        //--------------------- Index --------------------
        [HttpGet]
        public IActionResult Index()
        {
            var oderList= _db.Oders.Include(o => o.User).Include(c => c.Product).ToList();
            return View(oderList);
        }
        
        //--------------------- UpSert -------------------
        [NonAction]
        private IEnumerable<SelectListItem> UserSelectListItems()
        {
            var userList = _db.Users.ToList();
            var result = userList.Select(user => new SelectListItem
            {
                Text = user.Name,
                Value = user.UserId.ToString()
            });

            return result;
        }
        
        [NonAction]
        private IEnumerable<SelectListItem> ProductSelectListItems()
        {
            var productList = _db.Products.ToList();
            var result = productList.Select(product => new SelectListItem
            {
                Text = product.Name,
                Value = product.ProductId.ToString()
            });

            return result;
        }

        [HttpGet]
        public IActionResult UpSert(int? id)
        {
            OderViewModels oderVm = new OderViewModels();
            oderVm.UserList = UserSelectListItems();
            oderVm.ProductList = ProductSelectListItems();
            if (id == 0 || id == null)
            {
                oderVm.Oder = new Oder();
                return View(oderVm);
            }
        
            oderVm.Oder = _db.Oders.Find(id);
            return View(oderVm);
        }

        [HttpPost]
        public IActionResult UpSert(OderViewModels oderViewModels)
        {
            if (!ModelState.IsValid)
            {
                oderViewModels.UserList = UserSelectListItems();
                oderViewModels.ProductList = ProductSelectListItems();
                return View(oderViewModels);
            }

            if (oderViewModels.Oder.Id == 0 ||oderViewModels.Oder.Id == null)
            {
                _db.Oders.Add(oderViewModels.Oder);
            }
            else
            {
                _db.Oders.Update(oderViewModels.Oder);
            }

            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        
        //--------------------- Delete -------------------
        public IActionResult Delete(int id)
        {
            var oder = _db.Oders.Find(id);
            _db.Oders.Remove(oder);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}