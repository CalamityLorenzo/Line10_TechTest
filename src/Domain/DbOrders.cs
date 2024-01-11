using Domain.Entities;

namespace Domain
{
    public interface IDbOrders
    {
        public IEnumerable<Order> GetByCustomer(int id);
        public IEnumerable<Order> GetByProduct(int id);
        public IEnumerable<Order> GetByCustomer(Customer customer);
        public IEnumerable<Order> GetByProduct(Product product);
        public Order Add(Order customer);
        public Order Replace(Order customer);
        public Order Update(Order customer);
        public void Delete();
    }
}
