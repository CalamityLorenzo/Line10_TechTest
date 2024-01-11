using Domain.Db;

namespace Domain.Entities
{
    public class DomainRepo(IDbRepository db)
    {
        public CustomersRepo Customers => new CustomersRepo(db);
        public OrdersRepo Orders => new OrdersRepo(db);
        public ProductsRepo Products => new ProductsRepo(db);
    }
}
