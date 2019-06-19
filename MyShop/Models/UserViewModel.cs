using MyShop.Models.MyShopModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class UserViewModel
    {
        public User user { get; set; }
        public List <Credential> allCredential { get; set; }
        public int[] SelectedCredential { get; set; }
        public UserViewModel(User user, List<Credential> allCredential)
        {
            this.user = user;
            this.allCredential = allCredential;
        }
        public UserViewModel()
        {
            allCredential = new List<Credential> { };
        }
    }
}