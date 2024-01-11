using Domain.Entities;

namespace Domain
{
    public abstract class DbRepository
    {
        public DbCustomers Customers { get; }
        public DbProducts Products { get; }
        public DbOrders Orders { get; }
    }
}
