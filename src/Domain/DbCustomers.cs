using Domain.Entities;

namespace Domain
{
    public abstract class DbCustomers
    {
        public abstract Customer Get(int id);
        public abstract Customer Add(Customer customer);
        public abstract Customer Replace(Customer customer);
        public abstract Customer Update(Customer customer);
        public abstract void Delete();
    }
}
