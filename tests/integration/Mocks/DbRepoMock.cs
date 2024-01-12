using Domain.Db;

namespace integration.Mocks
{
    public class DbRepoMock : IDbRepository
    {
        public IDbCustomers Customers => new DbCustomersMock();

        public IDbProducts Products => new DbProductsMock();

        public IDbOrders Orders => new DbOrdersMock();
    }
}
