namespace Store.Logic.ProductStore.Service.ModifyServices
{
    using Infustructure;
    using Models.ModifyModels;

    public interface IProductCategoryModifyService : IObject, IModifyService<ProductCategoryModifyModel>
    {
        bool Delete(int key, out string reason);
    }
}
