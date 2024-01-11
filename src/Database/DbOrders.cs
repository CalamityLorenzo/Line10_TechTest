using Database.DbEntities;
using Domain.Db;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    internal class DbOrders(LineTenDbContext context) : IDbOrders
    {
        private Order ToClient(OrderDb order) => new Order(order.ProductId, order.CustomerId, order.Status, order.CreateDate, order.UpdatedDate);

        private OrderDb ToDb(Order order) => new OrderDb
        {
            ProductId = order.ProductId,
            CustomerId = order.CustomerId,
            Status = order.Status,
            CreateDate = order.CreatedDate,
            UpdatedDate = order.UpdatedDate,
        };

        public Order Add(Order customer)
        {
            var result = context.Orders.Add(ToDb(customer));
            context.SaveChanges();
            return ToClient(result.Entity);
        }

        public void Delete(Order order)
        {
            context.Orders.Remove(ToDb(order));
            context.SaveChanges();
        }

        public Order Get(int customerId, int productId)
        {
            var orderDb = context.Orders.AsNoTracking().First(a => a.CustomerId == customerId && a.ProductId == productId);
            return ToClient(orderDb);
        }

        public Order Update(Order order)
        {
            var result = context.Orders.Update(ToDb(order));
            context.SaveChanges();
            return ToClient(result.Entity); 
        }
    }
}