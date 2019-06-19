namespace Store.Logic.ProductStore.Models.ModifyModels
{
    public class ProductCategoryModifyModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public ProductCategoryModifyModel ()
        {

        }
        public ProductCategoryModifyModel(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }
    }
}