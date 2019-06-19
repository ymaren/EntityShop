namespace Store.Logic.ProductStore.Infustructure
{
    using Core.Common.Dal;
    using ProductStore.Factories;

    public sealed class ObjectFactoryBuilder
    {
        private IRepositoryFactory _sourceFactory;
        public void AddSource(IRepositoryFactory sourceFactory)
        {
            _sourceFactory = sourceFactory;
        }

        public IObjectFactory Build()
        {
            return new InternalDefaultObjectFactory(_sourceFactory);
        }
    }
}
