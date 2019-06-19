namespace Store.Logic.ProductStore.Infustructure
{
    public interface IModifyService<TModifyModel>
    {
        bool Add(TModifyModel entity);

        bool Update(int id, TModifyModel entity);

        bool Delete(int key);
    }
}
