using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Models.MyShopModels
{
   public class User
    {
        public int    Id { get; set; }
        [Required]
        public string UserEmail { get; set; }
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string UserCountry { get; set; }
        [Required]
        public string UserAddress { get; set; }
        public int      UserRoleId { get; set; }
        public  UserRole UserRole { get; set; }
        public List<Credential> Credential { get; set; }

        public User()
        {
            Credential = new List<Credential> { };
        }
    }
}
