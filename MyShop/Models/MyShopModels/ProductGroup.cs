using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Models.MyShopModels
{
   public class ProductGroup
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public string GroupDescription { get; set; }
        public int ProductCategoryid { get; set; }
        public virtual  ProductCategory ProductCategory { get; set; }
        public   ICollection<Product> Products;
        public ProductGroup ()
        {
            Products = new List<Product> ();
        }
    }
}
