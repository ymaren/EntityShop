using MyShop.Models.MyShopModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Providers.Entities;

namespace MyShop.Models
{
    public class OrderUser
    {
        public MyShop.Models.MyShopModels.User user { get; set; }
        public Order order { get; set; }

        public OrderUser(Order order, MyShop.Models.MyShopModels.User user)
        {
            this.order = order;
            this.user = user;
        }
    }
}