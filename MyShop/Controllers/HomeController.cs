using MyShop.Models.MyShopModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MyShop.Models;
using Microsoft.AspNet.Identity;

namespace MyShop.Controllers
{
    public enum Order
    {
        
        not_sort ,
        first_cheap,
        first_expensive
    };

    public class HomeController : Controller
    {
        DBContext _db;
        private int pageSize = 8;
        public HomeController()
        {
           _db  = new DBContext();
        }
            
        public ActionResult Index(int? category, int? group, Order sort= Order.not_sort, int page = 1)
        {
            IEnumerable<Product> products = _db.Products.Where(c => category == null || c.ProductGroup.ProductCategoryid == category).
                Where(g => group == null || g.ProductGroupId == group);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = products.Count() };
            switch (sort)
            {
                case Order.not_sort:
                    products = products.OrderBy(C => C.Id).Skip((page - 1) * pageSize).Take(pageSize);
                break;
                case Order.first_expensive:
                    products = products.OrderByDescending(C => C.Price).Skip((page - 1) * pageSize).Take(pageSize);
                break;
                case Order.first_cheap:
                    products = products.OrderBy(C => C.Price).Skip((page - 1) * pageSize).Take(pageSize);
                break;
               


            }
            ViewBag.Name = "Vika";

            IndexProductViewModel ivm = new IndexProductViewModel
            {
                PageInfo = pageInfo,
                Products = products,
                CurrentCategory = _db.ProductCategories.Where(c => c.Id == category).FirstOrDefault(),
                CurrentGroup = _db.ProductGroups.Where(g => g.Id == group).FirstOrDefault(),
                CurrentOrder = sort
            };


            return View(ivm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Menu()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                string email = User.Identity.GetUserName();
                return PartialView(_db.Users.Include(u=>u.Credential).FirstOrDefault(c=>c.UserEmail==email).Credential);
            }
            return RedirectToAction("Login", "Account");
            
        }
        public PartialViewResult VerticalMenu()
        {
          return PartialView(_db.ProductCategories.Include(g=>g.ProductGroups));
        }

    }
}