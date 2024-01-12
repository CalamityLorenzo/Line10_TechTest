using Domain.Db;
using Domain.Entities;

namespace integration.Mocks
{
    public class DbOrdersMock : IDbOrders
    {
        public Order Add(Order Order) => Order with { CustomerId = 10, ProductId = 20 };

        public void Delete(int id) { }

        public void Delete(Order Order) { }

        public Order Get(int customerId, int productId) => new Order(productId, customerId, "Status a", DateTimeOffset.Parse("01/05/81"), DateTimeOffset.Parse("01/05/96"));

        public Order Update(Order Order) => new Order(Order.ProductId, Order.CustomerId, "Status UPDATED", DateTimeOffset.Parse("01 / 05 / 32"), DateTimeOffset.Parse("01 / 05 / 96"));
    }
}