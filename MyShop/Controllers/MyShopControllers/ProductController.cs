
using MyShop.Models.MyShopModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Data.Entity;

namespace StoreWeb.Controllers
{
    [Authorize(Roles = "dicProduct")]
    public class ProductController : Controller
    {

        DBContext _db;
       
        public ProductController()
        {
            _db = new DBContext();           

        }

        public ViewResult Index()
        {
            List<Product> products = _db.Products.ToList();
           return View(products);
        }
        [HttpGet]
        public ViewResult Edit(int? Id)
        {
            SelectList groups = new SelectList(_db.ProductGroups, "Id", "GroupName");
            ViewBag.Groups = groups;
            Product product = _db.Products.FirstOrDefault(p => p.Id == Id);
            return View(product);
        }
        [HttpGet]
        public string Load(int? Id)
        {
            SelectList groups = new SelectList(_db.ProductGroups, "Id", "GroupName");
            ViewBag.Groups = groups;
            Product product = _db.Products.FirstOrDefault(p => p.Id == Id);
            return product.Name;
        }

        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase upload)
        {
            
            if (ModelState.IsValid)
            {
                Product foundProduct = _db.Products.AsNoTracking().FirstOrDefault(c => c.Id == product.Id);
                if (foundProduct!=null)
                {
                    
                    TempData["message"] = string.Format("Product \"{0}\" saved", product.Name);
                    _db.Entry(product).State = EntityState.Modified;
                    _db.SaveChanges();
                }
                else
                {
                    int  prod =_db.Products.Add(product).Id;
                    _db.SaveChanges();
                    TempData["message"] = string.Format("Product\"{0}\"saved", product.Name);
                }
                if (upload != null)
                {
                    // получаем имя файла
                    string fileName = System.IO.Path.GetFileName(upload.FileName);
                    // сохраняем файл в папку Pictures в проекте
                    upload.SaveAs(Server.MapPath("~/Pictures/"+product.Id+".jpg"));
                    TempData["message"] = string.Format("Foto add\"{0}\"", product.Name);
                    SelectList groups = new SelectList(_db.ProductGroups, "Id", "GroupName");
                    ViewBag.Groups = groups;
                    return RedirectToAction("Edit",new {@id= product.Id }); 
                }
                
                return RedirectToAction("Index");
            }
            else
            {
                SelectList groups = new SelectList(_db.ProductGroups, "Id", "GroupName");
                ViewBag.Groups = groups;
                return View(product);
            }
          
        }

        
        public ViewResult Create()
        {
            SelectList groups = new SelectList(_db.ProductGroups, "Id", "GroupName");
            ViewBag.Groups = groups;
            return View("Edit", new Product());
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            Product foundProduct = _db.Products.FirstOrDefault(c => c.Id == Id);

            if (foundProduct != null)
            {

                TempData["message"] = string.Format("Product   was deleted");
                _db.Products.Remove(foundProduct);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = string.Format("Product was not found");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public string Upload(HttpPostedFileBase upload, int? Id)
        {
            if (upload != null)
            {
                
                string fileName = System.IO.Path.GetFileName(upload.FileName);
               
                upload.SaveAs(Server.MapPath("~/Pictures/" + fileName));
            }
            return string.Empty;
        }
    }

}


   