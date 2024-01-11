namespace Domain
{
    public interface IDbRepository
    {
        public IDbCustomers Customers { get; }
        public IDbProducts Products { get; }
        public IDbOrders Orders { get; }
    }
}
