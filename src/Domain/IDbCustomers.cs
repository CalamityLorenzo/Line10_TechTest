using Domain.Entities;

namespace Domain
{
    public interface IDbCustomers
    {
        public Customer Get(int id);
        public Customer Add(Customer customer);
        public Customer Update(Customer customer);
        public void Delete(int id);
        public void Delete(Customer customer);
    }
}
