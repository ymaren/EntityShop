namespace Core.Dal.AdoNet.Repositories
{
    using Store.Logic.Entity;
    using System;
    using System.Data;

    internal class ProductGroupRepository : BaseRepository<ProductGroup>
    {
        public ProductGroupRepository(IDbConnection connection)
            : base(connection, "dbo.ProductGroups") { }

        public override bool Add(ProductGroup entity)
        {
            throw new NotImplementedException();
        }

        public override bool Update(ProductGroup entity)
        {
            throw new NotImplementedException();
        }
    }
}
