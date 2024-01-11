using Domain.Entities;

namespace Domain
{
    public interface IDbCustomers
    {
        public Customer Get(int id);
        public Customer Add(Customer customer);
        public Customer Replace(Customer customer);
        public Customer Update(Customer customer);
        public void Delete();
    }
}
