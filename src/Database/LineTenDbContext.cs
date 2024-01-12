using Database.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class LineTenDbContext : DbContext
    {
        internal DbSet<CustomerDb> Customers { get; set; }
        internal DbSet<OrderDb> Orders { get; set; }
        internal DbSet<ProductDb> Products{ get; set; }

        //public LineTenDbContext(DbContextOptions options) : base(options) { }

        public LineTenDbContext(DbContextOptions<LineTenDbContext> options): base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Customer Db Entitiy
            modelBuilder.Entity<CustomerDb>().HasKey(a => a.Id);
            modelBuilder.Entity<CustomerDb>().HasMany(e => e.CustomerOrders)
                .WithOne(a => a.Customer)
                .HasForeignKey(a => a.CustomerId)
                .HasPrincipalKey(a => a.Id);
                
            // Product Db Entitiy
            modelBuilder.Entity<ProductDb>().HasKey(a => a.Id);
            modelBuilder.Entity<ProductDb>()
                .HasMany(p => p.ProductOrders)
                .WithOne(o => o.Product)
                .HasForeignKey(o => o.ProductId)
                .HasPrincipalKey(p => p.Id);
            // Order Db Entitiy
            var orderEntity = modelBuilder.Entity<OrderDb>();
            //orderEntity.HasOne(a => a.Customer).WithMany(a => a.CustomerOrders).HasForeignKey(a => a.CustomerId).OnDelete(DeleteBehavior.NoAction);
            //orderEntity.HasOne(a => a.Product).WithMany(a => a.ProductOrders).HasForeignKey(a => a.ProductId).OnDelete(DeleteBehavior.NoAction);
            orderEntity.HasKey(a => new { a.ProductId, a.CustomerId });
        }
    }
}
