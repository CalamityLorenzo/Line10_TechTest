using Database.DbEntities;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    internal class DbCustomers(LineTenDbContext context) : IDbCustomers
    {

        CustomerDb ToDb(Customer customer) => new DbEntities.CustomerDb
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Phone = customer.Phone,
            Email = customer.Email
        };

        Customer ToClient(CustomerDb customer) => new Customer(

            Id: customer.Id,
            FirstName: customer.FirstName,
            LastName: customer.LastName,
            Phone: customer.Phone,
            Email: customer.Email);


        public Customer Add(Customer customer)
        {
            var result = context.Customers.Add(ToDb(customer));

            context.SaveChanges();
            // Never know if this is just cheeky.
            return customer with { Id = result.Entity.Id };
        }

        public void Delete(Customer customer)
        {
            context.Remove(ToDb(customer));
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var customer = Get(id);
            Delete(customer);
        }

        public Customer Get(int id)
        {
            var customerDb = context.Customers.AsNoTracking().First(a=>a.Id == id);
            return ToClient(customerDb);
        }


        public Customer Update(Customer customer)
        {
            var result = context.Customers.Update(ToDb(customer));
            return ToClient(result.Entity);
        }
    }
}