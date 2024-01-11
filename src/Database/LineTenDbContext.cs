using Database.DbEntities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class LineTenDbContext : DbContext
    {
        internal DbSet<CustomerDb> Customers { get; set; }
        internal DbSet<OrderDb> Orders { get; set; }
        internal DbSet<ProductDb> Products{ get; set; }

        public LineTenDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Customer Db Entitiy
            modelBuilder.Entity<CustomerDb>().HasKey(a => a.Id);

            // Product Db Entitiy
            modelBuilder.Entity<Product>().HasKey(a => a.Id);

            // Order Db Entitiy
            var orderEntity = modelBuilder.Entity<OrderDb>();
            orderEntity.HasKey(a => new { a.ProductId, a.CustomerId });
            orderEntity.HasOne(a => a.Customer).WithMany(a => a.CustomerOrders).HasForeignKey(a => a.CustomerId);
            orderEntity.HasOne(a => a.Product).WithMany(a => a.ProductOrders).HasForeignKey(a => a.ProductId);
        }
    }
}
