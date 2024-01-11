using Database;
using Microsoft.EntityFrameworkCore;

namespace integration
{
    public class DbIntegration
    {
        string _DbPath = "myFile.db";
        [Fact(DisplayName ="Instantiate db and then delete it.")]
        public void Instantiate_a_Conn_Repository()
        {

            DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder()
                                                                    .UseSqlite($"Data Source={_DbPath}")
                                                                    .EnableDetailedErrors(true);          
            LineTenDbContext context = new LineTenDbContext(dbContextOptionsBuilder.Options);
            Assert.True(context.Database.EnsureCreated());
            Assert.True(context.Database.EnsureDeleted());
        }



    }
}