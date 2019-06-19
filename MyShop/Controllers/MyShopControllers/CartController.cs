
using MyShop.Models;
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
    public class CartController : Controller
    {
        DBContext _db ;
        Cart cart;
       
        User currentUser;
       
        public CartController()
        {
            _db = new DBContext();
             
             cart = new Cart();
             currentUser = new User();
           


        }

        public RedirectToRouteResult AddToCart(Cart cart, int? Id, string returnUrl)
        {
            
            Product prod = _db.Products.FirstOrDefault(p => p.Id == Id);

            if (prod != null)
            {
                cart.AddItem(prod, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int? Id, string returnUrl)
        {
            Product prod = _db.Products.FirstOrDefault(p => p.Id == Id);

            if (prod != null)
            {
                cart.RemoveLine(prod);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

                
        public ViewResult Index(Cart cart, string returnUrl)
        {
            
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
               
            });
        }

        public ActionResult IndexSearch(int? Id)
        {
            ViewBag.OrderTypes = new SelectList(_db.OrderTypes, "Id", "OrderTypeName");

            var selected_orders = _db.Orders.Where(u => u.User.UserEmail == HttpContext.User.Identity.Name).
               Where(u => Id == null || u.OrderTypeId == Id).OrderBy(o => o.OrderDate).ThenBy(n => n.OrderNumber);


            return PartialView(selected_orders);
        }

        public ActionResult IndexSubSearchFilter(int? OrderTypeId)
        {
            
            ViewBag.OrderTypes = new SelectList(_db.OrderTypes, "Id", "OrderTypeName");
            var selected_orders = _db.Orders.Include(c=>c.OrderType).Where(u => u.User.UserEmail == HttpContext.User.Identity.Name).
               Where(u => OrderTypeId == null || u.OrderTypeId == OrderTypeId).OrderBy(o => o.OrderDate).ThenBy(n => n.OrderNumber);
            return PartialView(selected_orders);
        }



        public PartialViewResult _LoginPartial(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout(Cart cart, User user)
        {
                     
            User addOrChangeUser = null;
            Order newOrder=null;
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                addOrChangeUser  = _db.Users.FirstOrDefault(c=>c.UserEmail==HttpContext.User.Identity.Name);
                newOrder = CreateOrderForUser(addOrChangeUser.Id, cart);                
            }
            else
            {
                if (ModelState.IsValid)
                {
                    // check if user exist
                   addOrChangeUser = _db.Users.FirstOrDefault(c=>c.UserEmail== user.UserEmail);
                   if (addOrChangeUser != null)
                   {
                    //if user found update adress
                    _db.Entry(user).State = EntityState.Modified;
                     
                    }
                   else
                   {
                        // if user not exist add new user
                        user.UserRoleId = 3;
                     addOrChangeUser = _db.Users.Add(user);
                        _db.SaveChanges();
                   }
                    newOrder = CreateOrderForUser(addOrChangeUser.Id,  cart);
                                     
                }
                
            }
            if (HttpContext.User.Identity.IsAuthenticated||ModelState.IsValid)
            {
                newOrder.User = addOrChangeUser ?? new User();
                _db.Orders.Add(newOrder);
                _db.SaveChanges();
                cart.Clear();
            }

            
            return View(newOrder);
        }

        private Order CreateOrderForUser(int  user_id, Cart cart)
        {   

            Order newOrder = new Order
                  (DateTime.Now.Date,
                   GenerateOrderNumber(DateTime.Now.Date), 
                   user_id
                   , 1, cart.Lines.Sum(s => s.Quantity * s.Product.Price));

            newOrder.OrderDetail= cart.Lines.Select(line => new OrderDetail(
                     line.Product.Id,
                     line.Quantity,
                     line.Product.Price,
                     line.Quantity * line.Product.Price)).ToList();
            
            return newOrder;
           
        }


        private string GenerateOrderNumber(DateTime date)
        {
            int countOrderToday = _db.Orders.Where(d => d.OrderDate == date.Date).Count() + 1;
            return DateTime.Now.ToString("ddMMyyyy") + "_" + countOrderToday.ToString();
        }
    }
}