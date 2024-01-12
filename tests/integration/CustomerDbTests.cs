using Database;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace integration
{
    public class CustomerDbTests
    {

        string _DbPath = "customerTests.db";
        [Fact(DisplayName = "Add Customer")]
        public void Add_Customer()
        {
            var newCustomer = new Customer("Paul", "Lawrence", "0123456789", "e@mail.com");

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<LineTenDbContext>()
                                                                      .UseSqlite($"Data Source={_DbPath}")
                                                                    .EnableDetailedErrors(true);
            // We are responsible for the lifetime of the context here.
            using (LineTenDbContext context = new LineTenDbContext(dbContextOptionsBuilder.Options))
                try
                {
                    context.Database.EnsureCreated();
                    DbRepository dbRepo = new Database.DbRepository(context);
                    var dbCustomer = dbRepo.Customers.Add(newCustomer);
                    Assert.True(dbCustomer.Id > 0);
                    context.Database.EnsureDeleted();
                }
                catch (Exception ex)
                {
                    context.Database.EnsureDeleted();
                    throw;
                }
        }

        [Fact(DisplayName = "Update Customer")]
        public void Update_Customer()
        {
            var newCustomer = new Customer("Paul", "Lawrence", "0123456789", "e@mail.com");

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<LineTenDbContext>()
                                                                    .UseSqlite($"Data Source={_DbPath}")
                                                                    .EnableDetailedErrors(true);
            // We are responsible for the lifetime of the context here.
            using (LineTenDbContext context = new LineTenDbContext(dbContextOptionsBuilder.Options))
                try
                {
                    context.Database.EnsureCreated();
                    DbRepository dbRepo = new Database.DbRepository(context);
                    var dbCustomer = dbRepo.Customers.Add(newCustomer);
                    newCustomer = newCustomer with { FirstName = "Jerry" };
                    var updatedCustomer = dbRepo.Customers.Update(newCustomer);
                    Assert.True(updatedCustomer.FirstName == newCustomer.FirstName);
                    context.Database.EnsureDeleted();
                }
                catch (Exception ex)
                {
                    context.Database.EnsureDeleted();
                    throw;
                }
        }


        [Fact(DisplayName = "Fetch Customer")]
        public void Get_Customer()
        {


            var newCustomer1 = new Customer("Paul", "Lawrence", "0123456789", "e@mail.com");
            var newCustomer2 = new Customer("Jerry", "Lawrence", "0123456789", "e@mail.com");
            var newCustomer3 = new Customer("Kerry", "Lawrence", "0123456789", "e@mail.com");

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<LineTenDbContext>()
                                                                    .UseSqlite($"Data Source={_DbPath}")
                                                                    .EnableDetailedErrors(true);
            // We are responsible for the lifetime of the context here.
            using (LineTenDbContext context = new LineTenDbContext(dbContextOptionsBuilder.Options))
                try
                {
                    context.Database.EnsureCreated();
                    DbRepository dbRepo = new Database.DbRepository(context);
                    dbRepo.Customers.Add(newCustomer1);
                    dbRepo.Customers.Add(newCustomer2);
                    dbRepo.Customers.Add(newCustomer3);

                    var customer = dbRepo.Customers.Get(2);

                    Assert.True(customer.FirstName == newCustomer2.FirstName);
                }
                catch (Exception ex)
                {
                    context.Database.EnsureDeleted();
                    throw;
                }

        }


        [Fact(DisplayName = "Delete Customer")]
        public void Delete_Customer()
        {
            var newCustomer1 = new Customer("Paul", "Lawrence", "0123456789", "e@mail.com");
            var newCustomer2 = new Customer("Jerry", "Lawrence", "0123456789", "e@mail.com");
            var newCustomer3 = new Customer("Kerry", "Lawrence", "0123456789", "e@mail.com");

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<LineTenDbContext>()
                                                                                .UseSqlite($"Data Source={_DbPath}")
                                                                    .EnableDetailedErrors(true);
            // We are responsible for the lifetime of the context here.
            using (LineTenDbContext context = new LineTenDbContext(dbContextOptionsBuilder.Options))
            {
                try
                {
                    context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                    context.Database.EnsureCreated();
                    DbRepository dbRepo = new Database.DbRepository(context);
                    dbRepo.Customers.Add(newCustomer1);
                    dbRepo.Customers.Add(newCustomer2);
                    dbRepo.Customers.Add(newCustomer3);

                }
                catch (Exception ex)
                {
                    context.Database.EnsureDeleted();
                    throw;

                }

            }

            using (LineTenDbContext context = new LineTenDbContext(dbContextOptionsBuilder.Options))
            {
                try
                {

                    context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                    DbRepository dbRepo = new Database.DbRepository(context);
                    dbRepo.Customers.Delete(3);


                    var excep = Assert.Throws<System.InvalidOperationException>(() =>
                    {
                        dbRepo.Customers.Get(3);
                    });
                    // you can get various System.InvalidOperationException errors, ensure we have the correct error.
                    Assert.Equal("Sequence contains no elements", excep.Message);
                }
                catch (Exception ex)
                {
                    context.Database.EnsureDeleted();
                    throw;

                }
                context.Database.EnsureDeleted();

            }



        }

    }
}
