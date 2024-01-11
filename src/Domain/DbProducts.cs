using Domain.Entities;

namespace Domain
{
    public abstract class DbProducts
    {
        public abstract Product Get(int id);
        public abstract Product Add(Product customer);
        public abstract Product Replace(Product customer);
        public abstract Product Update(Product customer);
        public abstract void Delete(Product product);
        public abstract void Delete(int product);
    }
}
