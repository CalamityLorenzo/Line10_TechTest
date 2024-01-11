using Domain.Db;

namespace Domain.Entities
{
    public class CustomersRepo
    {
        private IDbRepository db;

        private void ValidateCustomer(Customer customer)
        {
            if (String.IsNullOrEmpty(customer.FirstName)) throw new ArgumentOutOfRangeException("Customer must have a FIRST name");
            if (String.IsNullOrEmpty(customer.LastName)) throw new ArgumentOutOfRangeException("Customer must have a LAST name");
            if (String.IsNullOrEmpty(customer.Phone)) throw new ArgumentOutOfRangeException("Customer must have a PHONE");
            if (String.IsNullOrEmpty(customer.Email)) throw new ArgumentOutOfRangeException("Customer must have an EMAIL");
        }

        public CustomersRepo(IDbRepository db)
        {
            this.db = db;
        }

        public Customer Get(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException("Id does not match a valid customer");
            return db.Customers.Get(id);
        }


        public Customer Add(Customer customer)
        {
            if (customer.Id > 0) throw new InvalidDataException("Customer alreadt exists");
            ValidateCustomer(customer);

            return db.Customers.Add(customer);
        }

        public Customer Update(Customer customer)
        {
            if (customer.Id <= 0) throw new ArgumentOutOfRangeException("Id does not match a valid customer");
            return db.Customers.Update(customer);
        }

        public void Delete(Customer customer)
        {
            if (customer.Id <= 0) throw new ArgumentOutOfRangeException("Id does not match a valid customer");
            db.Customers.Delete(customer);
        }
    }
}