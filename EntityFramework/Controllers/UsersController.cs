using System.Linq;
using EntityFramework.Data;
using EntityFramework.Models;
using Microsoft.AspNetCore.Mvc;

namespace EntityFramework.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _db;

        public UsersController(ApplicationDbContext db)
        {
            _db = db;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            var userList = _db.Users.ToList();
            return View(userList);
        }
        
        //Create
        [HttpGet]
        public IActionResult Create()
        {
            return View(new User());
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        
        //Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var user = _db.Users.Find(id);
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            _db.Users.Update(user);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        
        //UpSert
        [HttpGet]
        public IActionResult UpSert(int? id)
        {
            //Cho insert/ create
            if (id == 0 || id == null)
            {
                return View(new User());
            }
            
            //Cho update/ edit
            var user = _db.Users.Find(id);
            return View(user);
        }

        [HttpPost]
        public IActionResult UpSert(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            
            if (user.UserId == 0 || user.UserId == null)
            {
                _db.Users.Add(user);
            }
            else
            {
                _db.Users.Update(user);
            }
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        
        //Delete
        [HttpGet]
        public IActionResult Delete(int userId)
        {
            var user = _db.Users.Find(userId);
            _db.Users.Remove(user);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}