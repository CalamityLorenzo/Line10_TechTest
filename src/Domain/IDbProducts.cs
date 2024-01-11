using Domain.Entities;

namespace Domain
{
    public interface IDbProducts
    {
        public Product Get(int id);
        public Product Add(Product customer);
        public Product Replace(Product customer);
        public Product Update(Product customer);
        public void Delete(Product product);
        public void Delete(int product);
    }
}
