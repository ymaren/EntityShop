using MyShop.Models.MyShopModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class RoleViewModel
    {
        public UserRole userRole { get; set; }
        public List <Credential> allCredential { get; set; }
        public int[] SelectedCredential { get; set; }
        public RoleViewModel(UserRole userRole, List<Credential> allCredential)
        {
            this.userRole = userRole;
            this.allCredential = allCredential;
        }
        public RoleViewModel()
        {
            allCredential = new List<Credential> { };
        }
    }
}