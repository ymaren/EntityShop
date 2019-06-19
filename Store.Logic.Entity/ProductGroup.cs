namespace Store.Logic.Entity
{
    public class ProductGroup
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public ProductCategory Category { get; set; }
    }
}