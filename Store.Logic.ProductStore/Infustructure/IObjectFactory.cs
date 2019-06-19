namespace Store.Logic.ProductStore.Infustructure
{
    public interface IObjectFactory
    {
        TObject Create<TObject>() where TObject : IObject;
    }
}
