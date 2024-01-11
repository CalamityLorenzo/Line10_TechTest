﻿using Domain;

namespace Database.DbEntities
{
    internal class CustomerDb
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required  string LastName { get; set; }
        public required string Phone { get; set; }
        public required string Email { get; set; }
        public List<OrderDb> CustomerOrders { get; } = new();

    }
}
