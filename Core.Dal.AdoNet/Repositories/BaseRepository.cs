namespace Core.Dal.AdoNet.Repositories
{
    using Core.Common.Dal;
    using Exceptions;
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.Data;

    internal abstract class BaseRepository<TEntity> : IGenericRepository<TEntity, int>
        where TEntity : class, new()
    {
        private readonly IDbConnection _connection;
        private readonly List<IPropertyMapper<TEntity>> _mappers = new List<IPropertyMapper<TEntity>>();

        protected string BaseTable { get; }

        protected string BaseQuery => $"select * from {BaseTable}";

        protected void BindViewMapper(string field, Action<object, TEntity> mapper)
        {
            _mappers.Add(new PropertyMapper<TEntity>(field, mapper));
        }

        protected TEntity Map(IDataReader reader)
        {
            var entity = new TEntity();

            foreach (var mapper in _mappers)
            {
                mapper.Map(reader, entity);
            }

            return entity;
        }

        protected IDbConnection Connection
        {
            get
            {
                if (_connection.State != ConnectionState.Open)
                    _connection.Open();

                return _connection;
            }
        }

        public BaseRepository(IDbConnection connection, string tableName)
        {
            _connection = connection;
            BaseTable = tableName;
        }

        public IEnumerable<TEntity> GetAll()
        {
            var command = Connection.CreateCommand();
            command.CommandText = BaseQuery;
            try
            {
                using (var reader = command.ExecuteReader())
                {
                    var orders = new List<TEntity>();

                    while (reader.Read())
                        orders.Add(Map(reader));

                    return orders;
                }
            }
            catch (Exception ex)
            {
                throw new DalExecutionException("", ex);
            }
        }

        public TEntity GetSingle(int key)
        {
            var command = Connection.CreateCommand();
            command.CommandText = $"{BaseQuery} where Id ={key}";

            try
            {
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return Map(reader);
                    }

                    return null;
                }
            }
            catch (Exception e)
            {
                throw new DalExecutionException("", e);
            }
        }

        public bool Delete(int key)
        {
            var command = Connection.CreateCommand();
            command.CommandText = $"delete {BaseTable} where Id={key}";

            try
            {
                return command.ExecuteNonQuery() == 1;
            }
            catch (Exception e)
            {
                throw new DalExecutionException("", e);
            }
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }

        public abstract bool Add(TEntity entity);

        public abstract bool Update(TEntity entity);
    }
}