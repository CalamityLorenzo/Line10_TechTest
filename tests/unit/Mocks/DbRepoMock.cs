using Domain.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace unit.Mocks
{
    public class DbRepoMock : IDbRepository
    {
        public IDbCustomers Customers => new DbCustomersMock();

        public IDbProducts Products => new DbProductsMock();

        public IDbOrders Orders => new DbOrdersMock();
    }
}
