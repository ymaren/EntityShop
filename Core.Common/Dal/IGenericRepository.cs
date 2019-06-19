namespace Core.Common.Dal
{
    using System;
    using System.Collections.Generic;


    public interface IGenericRepository<TEntity, TKey> : IRepository, IDisposable
        where TEntity : class
    {
        TEntity GetSingle(TKey key);

        IEnumerable<TEntity> GetAll();

        bool Add(TEntity entity);

        bool Update(TEntity entity);

        bool Delete(TKey key);
    }
}
