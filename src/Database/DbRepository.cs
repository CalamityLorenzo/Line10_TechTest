using Domain;

namespace Database
{
    internal class DbRepository : IDbRepository
    {
      public DbRepository(LineTenDbContext context) { }

        public IDbCustomers Customers => throw new NotImplementedException();

        public IDbProducts Products => throw new NotImplementedException();

        public IDbOrders Orders => throw new NotImplementedException();
    }
}
