using Database;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace integration.Db
{
    public class OrderDbTests
    {
        Product newProduct1 = new("Product 1", "PRoduct 1 descrtition", "SKU_1");
        Product newProduct2 = new("Product 2", "PRoduct 1 descrtition", "SKU_2");
        Product newProduct3 = new("Product 3", "PRoduct 1 descrtition", "SKU_3");

        Customer newCustomer1 = new("Paul", "Lawrence", "0123456789", "e@mail.com");
        Customer newCustomer2 = new("Jerry", "Lawrence", "0123456789", "e@mail.com");
        Customer newCustomer3 = new("Kerry", "Lawrence", "0123456789", "e@mail.com");


        void AddOrderInfo(DbRepository repo)
        {
            repo.Customers.Add(newCustomer1);
            repo.Customers.Add(newCustomer2);
            repo.Customers.Add(newCustomer3);

            repo.Products.Add(newProduct1);
            repo.Products.Add(newProduct2);
            repo.Products.Add(newProduct3);
        }

        string _DbPath = "orderTests.db";
        [Fact(DisplayName = "Add Order - Success")]
        public void Add_Order()
        {
            var newOrder = new Order(1, 2, "Status", DateTimeOffset.Parse("01/01/2001"), DateTimeOffset.Parse("01/01/2002"));

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<LineTenDbContext>()
                                                                    .UseSqlite($"Data Source={_DbPath}")
                                                                    .EnableDetailedErrors(true);
            // We are responsible for the lifetime of the context here.
            using (LineTenDbContext context = new LineTenDbContext(dbContextOptionsBuilder.Options))
                try
                {
                    context.Database.EnsureCreated();
                    DbRepository dbRepo = new DbRepository(context);
                    AddOrderInfo(dbRepo);
                    var dbOrder = dbRepo.Orders.Add(newOrder);
                    Assert.True(dbOrder.CustomerId == newOrder.CustomerId && dbOrder.ProductId == newOrder.ProductId);
                    context.Database.EnsureDeleted();
                }
                catch (Exception ex)
                {
                    context.Database.EnsureDeleted();
                    throw;
                }
        }

        [Fact(DisplayName = "Add Order - NoCustomer")]
        public void Add_Order_NoCustomer_NoProduct()
        {
            var newOrder = new Order(1, 0, "Status", DateTimeOffset.Parse("01/01/2001"), DateTimeOffset.Parse("01/01/2002"));

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<LineTenDbContext>()
                                                                    .UseSqlite($"Data Source={_DbPath}")
                                                                    .EnableDetailedErrors(true);
            // We are responsible for the lifetime of the context here.
            using (LineTenDbContext context = new LineTenDbContext(dbContextOptionsBuilder.Options))
                try
                {
                    context.Database.EnsureCreated();
                    DbRepository dbRepo = new DbRepository(context);
                    //AddOrderInfo(dbRepo);
                    // dbRepo.Orders.Add(newOrder);
                    Assert.Throws<InvalidOperationException>(() => { dbRepo.Orders.Add(newOrder); });
                    context.Database.EnsureDeleted();
                }
                catch (Exception ex)
                {
                    context.Database.EnsureDeleted();
                    throw;
                }
        }

        [Fact(DisplayName = "Update Order")]
        public void Update_Order()
        {
            var newOrder = new Order(1, 2, "Status A", DateTimeOffset.Parse("01/01/2001"), DateTimeOffset.Parse("01/01/2002"));

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<LineTenDbContext>()
                                                                    .UseSqlite($"Data Source={_DbPath}")
                                                                    .EnableDetailedErrors(true);
            // We are responsible for the lifetime of the context here.
            using (LineTenDbContext context = new LineTenDbContext(dbContextOptionsBuilder.Options))
                try
                {
                    context.Database.EnsureCreated();
                    DbRepository dbRepo = new DbRepository(context);
                    AddOrderInfo(dbRepo);
                    dbRepo.Orders.Add(newOrder);
                }
                catch (Exception ex)
                {
                    context.Database.EnsureDeleted();
                    throw;
                }
            using (LineTenDbContext context = new LineTenDbContext(dbContextOptionsBuilder.Options))
                try
                {

                    DbRepository dbRepo = new DbRepository(context);
                    var newDbRecord = dbRepo.Orders.Get(2, 1);

                    var updatedDbRecord = dbRepo.Orders.Update(newDbRecord with { Status = "Status Wink" });
                    Assert.True(updatedDbRecord.Status == "Status Wink");
                }
                catch (Exception ex)
                {
                    context.Database.EnsureDeleted();
                    throw;
                }
        }

        [Fact(DisplayName = "Delete Order")]
        public void Delete_Order()
        {
            var newOrder = new Order(1, 2, "Status A", DateTimeOffset.Parse("01/01/2001"), DateTimeOffset.Parse("01/01/2002"));

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<LineTenDbContext>()
                                                                    .UseSqlite($"Data Source={_DbPath}")
                                                                    .EnableDetailedErrors(true);
            // We are responsible for the lifetime of the context here.
            using (LineTenDbContext context = new LineTenDbContext(dbContextOptionsBuilder.Options))
                try
                {
                    context.Database.EnsureCreated();
                    DbRepository dbRepo = new DbRepository(context);
                    AddOrderInfo(dbRepo);
                    dbRepo.Orders.Add(newOrder);
                }
                catch (Exception ex)
                {
                    context.Database.EnsureDeleted();
                    throw;
                }

            using (LineTenDbContext context = new LineTenDbContext(dbContextOptionsBuilder.Options))
                try
                {

                    DbRepository dbRepo = new DbRepository(context);
                    var newDbRecord = dbRepo.Orders.Get(2, 1);

                    dbRepo.Orders.Delete(newDbRecord);

                    var excep = Assert.Throws<InvalidOperationException>(() =>
                    {
                        dbRepo.Orders.Get(2, 1);
                    });
                    // you can get various System.InvalidOperationException errors, ensure we have the correct error.
                    Assert.Equal("Sequence contains no elements", excep.Message);
                }
                catch (Exception ex)
                {
                    context.Database.EnsureDeleted();
                    throw;
                }
        }
    }

}

