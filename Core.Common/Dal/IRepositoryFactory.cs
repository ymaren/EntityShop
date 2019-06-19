namespace Core.Common.Dal
{
    public interface IRepositoryFactory
    {
        IGenericRepository<TEntity, TKey> CreateRepository<TEntity, TKey>() where TEntity : class;
    }
}
