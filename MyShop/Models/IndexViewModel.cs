using MyShop.Models.MyShopModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class IndexProductViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PageInfo PageInfo { get; set; }
        public ProductCategory CurrentCategory { get; set; }
        public ProductGroup CurrentGroup { get; set; }
        public MyShop.Controllers.Order? CurrentOrder { get; set; }
    }
}