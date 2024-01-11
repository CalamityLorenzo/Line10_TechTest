using Domain;

namespace Database
{
    public class DbRepository(LineTenDbContext context) : IDbRepository
    {
        

        public IDbCustomers Customers => new DbCustomers(context);

        public IDbProducts Products => throw new NotImplementedException();

        public IDbOrders Orders => throw new NotImplementedException();
    }
}
