
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using MyShop.Models.MyShopModels;

namespace StoreWeb.Controllers
{
    [Authorize(Roles = "dicCategories")]
    public class CategoryController : Controller
    {

        DBContext _db;
        public CategoryController()
        {
            _db = new DBContext();
            
        }
        [HttpGet]
        public ViewResult Index()
        {
            return View(_db.ProductCategories);
        }

        [HttpGet]
        public ViewResult Edit(int? Id)
        {
            ProductCategory prodcategory = _db.ProductCategories.FirstOrDefault(c => c.Id == Id);
            return View(prodcategory);
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory category)
        {

            if (ModelState.IsValid)
            {
                ProductCategory foundCategory = _db.ProductCategories.AsNoTracking().FirstOrDefault(c => c.Id == category.Id);
                if (foundCategory!=null)
                {
                    
                    TempData["message"] = string.Format("Category \"{0}\"uploaded", category.CategoryName);
                    _db.Entry(category).State = EntityState.Modified;
                }
                else
                {
                    _db.ProductCategories.Add(category);
                    TempData["message"] = string.Format("Category\"{0}\"added", category.CategoryName);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {

                return View(category);
            }

        }

        [HttpPost]
        public ViewResult Create()
        {
            return View("Edit", new ProductCategory());
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
           
            ProductCategory foundCategory = _db.ProductCategories.FirstOrDefault(c => c.Id == Id);

            if (foundCategory!=null)
            {

                TempData["message"] = string.Format("Product category  was deleted");
                _db.ProductCategories.Remove(foundCategory);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = string.Format("Product category was not found");
            }

            return RedirectToAction("Index");
        }
    }

}


