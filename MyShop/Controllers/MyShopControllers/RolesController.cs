
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using MyShop.Models;
using MyShop.Models.MyShopModels;

namespace StoreWeb.Controllers
{
    public class RolesController : Controller
    {

        DBContext _db;
        List<Credential> allCredentials;
        
        public RolesController()
        {
            _db = new DBContext();
            allCredentials = new List<Credential> { };
            allCredentials = _db.Credentials.ToList();
        }

        //list
        public ViewResult Index()
        {
            return View(_db.UserRoles);
        }

       
        [HttpGet]
        public ViewResult Edit(int? Id)
        {
            
            UserRole role = _db.UserRoles.Include(c=>c.Credential).FirstOrDefault(c => c.Id == Id);
            

            return View(new RoleViewModel (role, allCredentials));
        }

        [HttpPost]
        public ActionResult Edit(RoleViewModel role)
        {
            if (role.SelectedCredential != null)
            {
                foreach (int item in role.SelectedCredential)
                {
                    role.userRole.Credential.Add(allCredentials.FirstOrDefault(c => c.Id == item));
                }
            }
            ModelState["userRole.Id"].Errors.Clear();
            if (ModelState.IsValid)
            {
                UserRole foundRole = _db.UserRoles.Include(c => c.Credential).FirstOrDefault(c => c.Id == role.userRole.Id);
                if (foundRole!=null)
                {
                    foundRole.Credential= role.userRole.Credential;
                    foundRole.UserRoleName = role.userRole.UserRoleName;

                    foreach (var cred in foundRole.Credential)
                    {
                        string st = _db.Entry(cred).State.ToString();
                    }

                    TempData["message"] = string.Format("Role \"{0}\"uploaded", role.userRole.UserRoleName);
                    _db.Entry(foundRole).State = EntityState.Modified;
                    _db.SaveChanges();
                }
                else
                {
                    _db.UserRoles.Add(role.userRole);
                    TempData["message"] = string.Format("Role\"{0}\"added", role.userRole.UserRoleName);
                    _db.SaveChanges();
                }
                
                return RedirectToAction("Index");
            }
            else
            {
                
                return View(role);
            }
          
        }

        [HttpPost]
        public ViewResult Create()
        {
            return View("Edit", new UserRole());
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            UserRole  foundRole = _db.UserRoles.FirstOrDefault(c => c.Id == Id);

            if (foundRole != null)
            {

                TempData["message"] = string.Format("Role  was deleted");
                _db.UserRoles.Remove(foundRole);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = string.Format("Role was not found");
            }

            return RedirectToAction("Index");
        }
    }

}


   