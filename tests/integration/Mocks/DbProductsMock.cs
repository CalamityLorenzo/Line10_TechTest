using Domain.Db;
using Domain.Entities;

namespace integration.Mocks
{
    public class DbProductsMock : IDbProducts
    {
        public Product Add(Product Product) => new Product(100, "Product 1", "The first product", "SKU 001");

        public void Delete(int id) { }

        public void Delete(Product Product) { }

        public Product Get(int id) => new Product(100, "Product 1", "The first product", "SKU 001");

        public Product Update(Product Product) => Product;
    }
}