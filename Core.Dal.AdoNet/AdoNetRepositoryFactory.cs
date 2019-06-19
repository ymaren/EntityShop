namespace Core.Dal.AdoNet
{
    using Common.Dal;
    using Repositories;
    using Store.Logic.Entity;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class AdoNetRepositoryFactory : IRepositoryFactory
    {
        private readonly string _constring;
        public AdoNetRepositoryFactory(string connection)
        {
            this._constring = connection;
        }

        private readonly static Dictionary<Type, Func<IDbConnection, IRepository>> _supportedObjects
            = new Dictionary<Type, Func<IDbConnection, IRepository>>
            {
                //[typeof(Product)] = (connection) => new ProductRepository(connection),
                [typeof(ProductCategory)] = (connection) => new ProductCategoryRepository(connection),
                [typeof(ProductGroup)] = (connection) => new ProductGroupRepository(connection)
                //[typeof(User)] = (connection) => new UserRepository(connection),
                //[typeof(Role)] = (connection) => new UserRoleRepository(connection),
                //[typeof(UserCredential)] = (connection) => new UserCredentialRepository(connection),
                //[typeof(OrderType)] = (connection) => new OrderTypeRepository(connection),
                //[typeof(OrderHeader)] = (connection) => new OrderHeaderRepository(connection),
                //[typeof(OrderDetail)] = (connection) => new OrderDetailRepository(connection),


            };

        public IGenericRepository<TEntity, Key> CreateRepository<TEntity, Key>() where TEntity : class
        {
            System.Func<IDbConnection, IRepository> delegateFactory = null;
            if (_supportedObjects.TryGetValue(typeof(TEntity), out delegateFactory))
            {
                return (IGenericRepository<TEntity, Key>)delegateFactory(new SqlConnection(_constring));
            }

            throw new System.NotImplementedException();
        }
    }
}
