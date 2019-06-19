using MyShop.Models.MyShopModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;


namespace StoreWeb.Controllers
{
    public class OrderController : Controller
    {

        DBContext _db;
       
        public OrderController()
        {
            _db = new DBContext();
           
        }

        public ViewResult Index()
        {
            ViewBag.OrderTypes = new SelectList(_db.OrderTypes, "Id", "OrderTypeName");
            ViewBag.Users = new SelectList(_db.Users, "Id", "UserName");
            return View(_db.Orders.Include(c=>c.OrderType).ToList());
        }

        public ActionResult IndexSearch(DateTime? StartDate, DateTime? FinishDate, int? orderToUser, int? OrderTypeId)
        {
           
            var selected_orders = _db.Orders.Where(u => orderToUser == null || u.UserId == orderToUser).
                Where(u => OrderTypeId == null || u.OrderTypeId == OrderTypeId).Where
                (d=> StartDate == null || d.OrderDate>=StartDate).Where(
                f => FinishDate == null || f.OrderDate <= FinishDate).OrderBy(o=>o.OrderDate).ThenBy(n=>n.OrderNumber);
            return PartialView(selected_orders.Include(c => c.OrderType).Include(c => c.User));
        }



        [HttpGet]
        public ViewResult Edit(int? Id)
        {
           //bool load=  DbContext.Configuration.LazyLoadingEnabled
            ViewBag.OrderTypes = new SelectList(_db.OrderTypes, "Id", "OrderTypeName");
            ViewBag.Users = new SelectList(_db.Users, "Id", "UserName");
            Order order = _db.Orders.Include(c => c.OrderType).Include(c=>c.OrderDetail.Select(p=>p.Product)).FirstOrDefault(c => c.Id == Id);
           
            
            return View(order??new Order(DateTime.Now, GenerateOrderNumber (DateTime.Now)));
        }

        private string GenerateOrderNumber (DateTime date)
        {
          int countOrderToday=  _db.Orders.Where(d => d.OrderDate == date.Date).Count()+1;
            return DateTime.Now.ToString("ddMMyyyy") + "_" + countOrderToday.ToString();
        }

        [HttpPost]
        public ActionResult Edit(Order order)
        {

            if (ModelState.IsValid)
            {
                Order foundOrder = _db.Orders.AsNoTracking().FirstOrDefault(c => c.Id == order.Id);
                if (foundOrder!=null)
                {
                    
                    TempData["message"] = string.Format("Order \"{0}\"uploaded", order.OrderNumber);
                    _db.Entry(foundOrder).State = EntityState.Modified;
                }
                else
                {
                    _db.Orders.Add(order);
                    TempData["message"] = string.Format("Order\"{0}\"added", order.OrderNumber);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return View(order);
            }

        }
        [HttpPost]
        public ViewResult Create()
        {
            return View("Edit", new Order());
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            Order foundOrder= _db.Orders.FirstOrDefault(c => c.Id == Id);

            if (foundOrder != null)
            {
                TempData["message"] = string.Format("Order was deleted");
                _db.Orders.Remove(foundOrder);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = string.Format("User was not found");
            }

            return RedirectToAction("Index");
        }


        public ViewResult CreateOrderFromCart()
        {
            return View("Edit", new Order());
        }
    }

}


