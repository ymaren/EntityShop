using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Models.MyShopModels
{
   public class Credential
    {
        public int Id { get; set; }
        public string NameCredential { get; set; }
        public string FullNameCredential { get; set; }
        public int ParentCredentialId { get; set; }
        public int Order { get; set; }
        public string Url { get; set; }
        public ICollection<UserRole> UserRole { get; set; }
        public ICollection<User> User { get; set; }
    }
}
