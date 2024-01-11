using Domain.Entities;

namespace Domain.Db
{
    public interface IDbOrders
    {

        //public IEnumerable<Order> GetByCustomer(int id);
        //public IEnumerable<Order> GetByProduct(int id);
        //public IEnumerable<Order> GetByCustomer(Customer customer);
        //public IEnumerable<Order> GetByProduct(Product product);
        public Order Get(int customerId, int productId);
        public Order Add(Order order);
        public Order Update(Order order);
        public void Delete(Order order);
    }
}
