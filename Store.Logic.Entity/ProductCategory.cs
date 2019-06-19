namespace Store.Logic.Entity
{
    using System.Collections.Generic;

    public class ProductCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<ProductGroup> ProductGroups { get; set; }
    }
}
