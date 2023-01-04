using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
           IEnumerable<Category> objCategoryList = _db.Categories;
           return View(objCategoryList);
        }
        
        //Create
        //GET
        public IActionResult Create()
        {
           return View();
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name==obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The display order can not have exactly match the name");
            }
            if (ModelState.IsValid)
            { 
            _db.Categories.Add(obj);
            _db.SaveChanges();
                TempData["Success"] = "Category Created Successfully";
            return RedirectToAction("Index");
            }
            return View(obj);
        }
        //Update
        //GET
        public IActionResult Edit(int? id)
        {
            if (id==null || id==0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);
          //  var categoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.Id == id);
            //var categoryFromDbsingle = _db.Categories.SingleOrDefault(u => u.Id == id);
            if (categoryFromDb==null)
            {
                return NotFound();
            }

            return View(categoryFromDb);

        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The display order can not have exactly match the name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["Success"] = "Category Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //Delete
        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);
            //  var categoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.Id == id);
            //var categoryFromDbsingle = _db.Categories.SingleOrDefault(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);

        }

        //Post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Categories.Find(id);
            if (obj==null)
            {
                return NotFound();

            }

                _db.Categories.Remove(obj);
                _db.SaveChanges();
            TempData["Success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");
           
          
        }

    }
}
