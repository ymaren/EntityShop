using MyShop.Models;
using MyShop.Models.MyShopModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace MyShop.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        DBContext _db;
        IEnumerable<User> users;
        public AccountController ()
        {
             _db = new DBContext();
             users = _db.Users;
        }

        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                string result = "Вы не авторизованы";
            }

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LogViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, true);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                       if (HttpContext.User.IsInRole("Admin"))
                        return RedirectToAction("Menu", "Home");
                       else
                       return RedirectToAction("Index", "Cart");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный пароль или логин");
                }
            }
            return View(model);
        }
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult Register(User newUser)
        {
            if (ModelState.IsValid)
            {
                 newUser.UserRoleId = 3;
                User foundUser = _db.Users.AsNoTracking().FirstOrDefault(c => c.Id == newUser.Id);
                if (foundUser != null)
                {
                    _db.Entry(newUser).State = EntityState.Modified;
                    FormsAuthentication.SignOut();
                    FormsAuthentication.SetAuthCookie(newUser.UserEmail, true);
                    _db.SaveChanges();
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    _db.Users.Add(newUser);
                    _db.SaveChanges();
                    return RedirectToAction("Login");
                }
                
            }
            return View(newUser);
        }
        public ActionResult Register(string returnUrl)
        {
            
            ViewBag.LastURL = returnUrl;
            if (HttpContext.User.Identity.IsAuthenticated)
                return View(_db.Users.FirstOrDefault(c => c.UserEmail == HttpContext.User.Identity.Name));


            return View(new User());
        }


        private bool ValidateUser(string login, string password)
        {
            bool isValid = false;

            using (DBContext _db = new DBContext())
            {
                try
                {
                    User user = _db.Users.FirstOrDefault(c=>c.UserEmail==login && c.UserPassword==password); 

                    if (user != null)
                    {
                        isValid = true;
                    }
                }
                catch
                {
                    isValid = false;
                }
            }
            return isValid;
        }
    }
}