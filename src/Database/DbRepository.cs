using Domain.Db;

namespace Database
{
    public class DbRepository(LineTenDbContext context) : IDbRepository
    {
        
        public IDbCustomers Customers => new DbCustomers(context);

        public IDbProducts Products => new DbProducts(context);

        public IDbOrders Orders => new DbOrders(context);
    }
}
