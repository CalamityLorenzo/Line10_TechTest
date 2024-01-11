using Database.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class LineTenDbContext : DbContext
    {
        internal DbSet<CustomerDb> Customers { get; set; }
        internal DbSet<OrderDb> Orders { get; set; }
        internal DbSet<ProductDb> Products{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
