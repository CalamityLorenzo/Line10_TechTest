﻿using Domain.Db;
using Domain.Entities;

namespace integration.Mocks
{
    public class DbCustomersMock : IDbCustomers
    {
        public Customer Add(Customer customer) => customer with { Id = 100 };

        public void Delete(int id) { }

        public void Delete(Customer customer) { }

        public Customer Get(int id) => new Customer(100, "Paul", "Get", "1", "2@mail.com");

        public Customer Update(Customer customer) => customer;
    }
}