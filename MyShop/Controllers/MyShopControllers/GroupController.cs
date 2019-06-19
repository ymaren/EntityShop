

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Data.Entity;
using MyShop.Models.MyShopModels;

namespace StoreWeb.Controllers
{
    [Authorize(Roles = "dicProductGroup")]
    public class GroupController : Controller
    {

        DBContext _db;
      
        public GroupController()
        {
            _db = new DBContext();
          
        }

        [HttpGet]
        public ViewResult Index()
        {
            //IEnumerable<ProductGroup> group;
            //using (DBContext _d = new DBContext())
            //{
            //    //group = _db.ProductGroups.Include(c => c.ProductCategory)

            //    //foreach (ProductGroup g in group)
            //    //{
            //    //    string st = g.ProductCategory.CategoryName;
            //    //}
            //}
            return View(_db.ProductGroups.Include(c => c.ProductCategory));
        }

        //edit one group
        [HttpGet]
        public ViewResult Edit(int? Id)
        {
            SelectList categorylist = new SelectList(_db.ProductCategories, "Id", "CategoryName");
            ViewBag.Categories = categorylist;
            ProductGroup group = _db.ProductGroups.FirstOrDefault(c => c.Id == Id);
            return View(group);
        }

        [HttpPost]
        public ActionResult Edit(ProductGroup group)
        {
            
            if (ModelState.IsValid)
            {
                ProductGroup foundGroup = _db.ProductGroups.AsNoTracking().FirstOrDefault(c => c.Id == group.Id);
                if (foundGroup!=null)
                {
                    
                    TempData["message"] = string.Format("Group \"{0}\"uploaded", group.GroupName);
                    _db.Entry(group).State = EntityState.Modified;
                }
                else
                {
                    _db.ProductGroups.Add(group);
                    TempData["message"] = string.Format("Group\"{0}\"added", group.GroupName);
                }
                _db.SaveChanges();
               return RedirectToAction("Index");
            }
            else
            {
                
                return View(group);
            }
          
        }

        [HttpPost]
        public ViewResult Create()
        {
            return View("Edit", new ProductGroup());
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
           

            ProductGroup foundGroup = _db.ProductGroups.FirstOrDefault(c => c.Id == Id);

            if (foundGroup!=null)
            {
                TempData["message"] = string.Format("Product group  was deleted");
                _db.ProductGroups.Remove(foundGroup);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            { 
                TempData["message"] = string.Format("Product group was not found");
            }
            return RedirectToAction("Index");
        }
    }

}


   