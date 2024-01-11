using Domain.Entities;

namespace Domain.Db
{
    public interface IDbProducts
    {
        public Product Get(int id);
        public Product Add(Product product);
        public Product Update(Product product);
        public void Delete(Product product);
        public void Delete(int product);
    }
}
