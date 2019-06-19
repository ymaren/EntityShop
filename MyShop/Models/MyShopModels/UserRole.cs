using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MyShop.Models.MyShopModels
{
    //[Bind(Exclude ="Id")]
  public  class UserRole
    {
        public int Id { get; set; }

        [Required]
        public string UserRoleName { get; set; }
        public List<Credential> Credential { get; set; }
        public  ICollection<User> Users { get; set; }
        public UserRole(string userRoleName)
        {
          this.UserRoleName = userRoleName;
        }

        public UserRole()
        {
            Credential = new List<Credential> { };
            Users = new List<User> ();
        }
    }
}
