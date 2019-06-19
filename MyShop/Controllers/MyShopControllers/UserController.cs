
using MyShop.Models.MyShopModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MyShop.Models;

namespace StoreWeb.Controllers
{
    //[Authorize(Roles = "dicUser")]
    public class UserController : Controller
    {

        DBContext _db;
        List<Credential> allCredentials;
        public UserController()
        {
            _db = new DBContext();
            allCredentials = new List<Credential> { };
            allCredentials = _db.Credentials.ToList();

        }

        // GET: User
        public ActionResult Index()
        {
            ViewBag.Roles = new SelectList(_db.UserRoles, "Id", "UserRoleName");
            List<User> users = _db.Users.Include(c => c.UserRole).ToList();
            return View(users);
        }
      
        public ActionResult IndexSearch(int? UserRoleId)
        {
            ViewBag.Roles = new SelectList(_db.UserRoles, "UserRoleId", "UserRoleName");
            var users = _db.Users.Where(u => UserRoleId == null||  u.UserRoleId== UserRoleId);
            return PartialView(users.Include(c => c.UserRole));
        }

        public ActionResult RoleTemplateIndexSearch(int? UserRoleId)
        {
            ViewBag.Roles = new SelectList(_db.UserRoles, "UserRoleId", "UserRoleName");
            var user = _db.Users.FirstOrDefault();
            if (UserRoleId != null)
            {
                var role = _db.UserRoles.Include(c => c.Credential).First(r => UserRoleId == null || r.Id == UserRoleId);
                user.Credential = role.Credential;
            }
            return PartialView(new UserViewModel(user, allCredentials));
        }

        public ActionResult CreateChange(int? Id)
        {          
            
            User user = _db.Users.Include(c=>c.UserRole).Include(c => c.Credential).FirstOrDefault(u => u.Id == Id);

            SelectList roles = new SelectList(_db.UserRoles, "Id", "UserRoleName",user!=null?user.UserRoleId:0);
            ViewBag.Roles = roles;
            return View( new UserViewModel (user, allCredentials));
        }

        [HttpPost]
        public ActionResult CreateChange(UserViewModel userViewModel)
        {
            if (userViewModel.SelectedCredential != null)
            {
                foreach (int item in userViewModel.SelectedCredential)
                {
                    userViewModel.user.Credential.Add(allCredentials.FirstOrDefault(c => c.Id == item));
                }
            }

            ModelState["user.Id"].Errors.Clear();
            if (ModelState.IsValid)
            {
                User foundUser = _db.Users.Include(c => c.Credential).FirstOrDefault(c => c.Id == userViewModel.user.Id);
                if (foundUser!=null)
                {
                    foundUser.Credential = userViewModel.user.Credential;
                    foundUser.UserName = userViewModel.user.UserName;
                    foundUser.UserPassword = userViewModel.user.UserPassword;
                    foundUser.UserRoleId = userViewModel.user.UserRoleId;
                    TempData["message"] = string.Format("User \"{0}\" uploaded", userViewModel.user.UserEmail);
                    _db.Entry(foundUser).State = EntityState.Modified;
                    _db.SaveChanges();
                }
                else
                {
                    _db.Users.Add(userViewModel.user);
                    TempData["message"] = string.Format("User\"{0}\" uploaded ", userViewModel.user.UserEmail);
                   _db.SaveChanges();
                }
                
                return RedirectToAction("Index");
            }
            else
            {
                SelectList roles = new SelectList(_db.UserRoles, "Id", "UserRoleName", userViewModel.user != null ? userViewModel.user.UserRoleId : 0);
                ViewBag.Roles = roles;
                // Что-то не так со значениями данных
                return View(userViewModel);
            }
        }

        [HttpPost]
        public ActionResult Delete(int UserId)
        {
            User foundUser = _db.Users.FirstOrDefault(c => c.Id == UserId);

            if (foundUser != null)
            {

                TempData["message"] = string.Format("User  was deleted");
                _db.Users.Remove(foundUser);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = string.Format("User was not found");
            }

            return RedirectToAction("Index");
        }


}
}