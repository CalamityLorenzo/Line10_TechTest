using Database;
using Microsoft.EntityFrameworkCore;

namespace integration
{
    public class CustomerDbTests
    {
        string _DbPath = "customerTests.db";
        [Fact(DisplayName = "Add Customer.")]
        public void Add_Customer()
        {

            DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder()
                                                                    .UseSqlite($"Data Source={_DbPath}")
                                                                    .EnableDetailedErrors(true);
            LineTenDbContext context = new LineTenDbContext(dbContextOptionsBuilder.Options);
            Assert.True(context.Database.EnsureCreated());

        }
    }
}
