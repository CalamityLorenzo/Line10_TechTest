using Domain.Entities;

namespace Domain
{
    public abstract class DbOrders
    {
        public abstract IEnumerable<Order> GetByCustomer(int id);
        public abstract IEnumerable<Order> GetByProduct(int id);
        public abstract IEnumerable<Order> GetByCustomer(Customer customer);
        public abstract IEnumerable<Order> GetByProduct(Product product);
        public abstract Order Add(Order customer);
        public abstract Order Replace(Order customer);
        public abstract Order Update(Order customer);
        public abstract void Delete();
    }
}
