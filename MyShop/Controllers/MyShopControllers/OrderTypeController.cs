
using MyShop.Models.MyShopModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace StoreWeb.Controllers
{
    public class OrderTypeController : Controller
    {

        DBContext _db;
        
        
        public OrderTypeController()
        {
            _db = new DBContext();
           
        }

        //list
        public ViewResult Index()
        {
            return View(_db.OrderTypes);
        }

        
        [HttpGet]
        public ViewResult Edit(int? Id)
        {

            OrderType orderType = _db.OrderTypes.FirstOrDefault(c => c.Id == Id);
            return View(orderType);
        }

        [HttpPost]
        public ActionResult Edit(OrderType orderType)
        {
            
            if (ModelState.IsValid)
            {
                OrderType foundOrderType= _db.OrderTypes.AsNoTracking().FirstOrDefault(c => c.Id == orderType.Id);
                if (foundOrderType!=null)
                {
                    _db.Entry(orderType).State = EntityState.Modified;
                    TempData["message"] = string.Format("Order type \"{0}\"uploaded", orderType.OrderTypeName);
                    
                }
                else
                {
                    _db.OrderTypes.Add(orderType);
                    TempData["message"] = string.Format("Order type\"{0}\"added", orderType.OrderTypeName);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                
                return View(orderType);
            }
          
        }

        [HttpPost]
        public ViewResult Create()
        {
            return View("Edit", new OrderType());
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {

            OrderType foundType = _db.OrderTypes.FirstOrDefault(c => c.Id == Id);

            if (foundType != null)
            {
                TempData["message"] = string.Format("Product group  was deleted");
                _db.OrderTypes.Remove(foundType);
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


   