namespace Core.Dal.AdoNet.Repositories
{
    using Core.Dal.AdoNet.Exceptions;
    using Store.Logic.Entity;
    using System;
    using System.Data;

    internal class ProductCategoryRepository : BaseRepository<ProductCategory>
    {
        const string IdField = "Id";
        const string CategoryNameFild = "CategoryName";
        const string CategoryDescriptionFiled = "CategoryDescription";
        


        public ProductCategoryRepository(IDbConnection connection)
            : base(connection, "dbo.ProductCategories")
        {
            BindViewMapper(IdField, (o, e) => e.Id = Convert.ToInt32(o));
            BindViewMapper(CategoryNameFild, (o, e) => e.Name = o.ToString());
            BindViewMapper(CategoryDescriptionFiled, (o, e) => e.Description = o.ToString());
           
        }

        public override bool Add(ProductCategory entity)
        {
            var command = Connection.CreateCommand();
            command.CommandText = $"insert into {BaseTable} " +
                $"({CategoryNameFild}, {CategoryDescriptionFiled}) values " +
                $"( '{entity.Name}', '{entity.Description}') ";
            try
            {
                return command.ExecuteNonQuery() == 1;
            }
            catch (Exception e)
            {
                throw new DalExecutionException("", e);
            }
        }

        public override bool Update(ProductCategory entity)
        {
            var command = Connection.CreateCommand();
            command.CommandText = $"update {BaseTable} " +
                $"set " +
                $"{CategoryNameFild}='{entity.Name}', " +
                $"{CategoryDescriptionFiled}='{entity.Description}' where Id={entity.Id}";
            try
            {
                return command.ExecuteNonQuery() == 1;
            }
            catch (Exception e)
            {
                throw new DalExecutionException("", e);
            }
        }
    }
}