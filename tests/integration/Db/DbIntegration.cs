using Database;
using Microsoft.EntityFrameworkCore;

namespace integration.Db
{
    public class DbIntegration
    {
        string _DbPath = "myFile.db";
        [Fact(DisplayName = "Instantiate db and then delete it.")]
        public void Instantiate_a_Conn_Repository()
        {

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<LineTenDbContext>()
                                                                    .UseSqlite($"Data Source={_DbPath}")
                                                                    .EnableDetailedErrors(true);
            // We are responsible for the lifetime of the context here.
            using LineTenDbContext context = new LineTenDbContext(dbContextOptionsBuilder.Options);
            Assert.True(context.Database.EnsureCreated());
            Assert.True(context.Database.EnsureDeleted());
        }



    }
}