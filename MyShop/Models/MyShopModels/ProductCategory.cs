using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Models.MyShopModels
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public virtual ICollection<ProductGroup> ProductGroups { get; set; }
        public ProductCategory()
        {
            ProductGroups = new List<ProductGroup>();
        }
        public ProductCategory(string categoryName, string categoryDescription)
        {

            this.CategoryName = categoryName;
            this.CategoryDescription = categoryDescription;

        }
      
    }
}
