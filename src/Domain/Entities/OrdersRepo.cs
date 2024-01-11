using Domain.Db;

namespace Domain.Entities
{
    public class OrdersRepo
    {
        private IDbRepository db;

        

        public OrdersRepo(IDbRepository db)
        {
            this.db = db;
        }

        public Order Get(int customerId, int productId)
        {
            if (customerId <= 0) throw new ArgumentOutOfRangeException("customer id does not match a valid Order");
            if (productId <= 0) throw new ArgumentOutOfRangeException("product Id does not match a valid Order");
            return db.Orders.Get(customerId, productId);
        }

        public Order Add(Order Order)
        {
            if (Order.CustomerId <= 0) throw new InvalidDataException("customer id is not valid");
            if (Order.ProductId <= 0) throw new InvalidDataException("product id is not valid");

            return db.Orders.Add(Order);
        }

        public Order Update(Order Order)
        {
            if (Order.CustomerId <= 0) throw new InvalidDataException("customer id is not valid");
            if (Order.ProductId <= 0) throw new InvalidDataException("product id is not valid");

            return db.Orders.Update(Order);
        }

        public void Delete(Order Order)
        {
            if (Order.CustomerId <= 0) throw new InvalidDataException("customer id is not valid");
            if (Order.ProductId <= 0) throw new InvalidDataException("product id is not valid");
            db.Orders.Delete(Order);
        }
    }
}