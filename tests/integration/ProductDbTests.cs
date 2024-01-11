using Database;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace integration
{
    public class ProductDbTests
    {
        string _DbPath = "productTests.db";
        [Fact(DisplayName = "Add Product")]
        public void Add_Product()
        {
            var newProduct= new Product("Prodcut 1", "PRoduct 1 descrtition", "SKU_1");

            DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder()
                                                                    .UseSqlite($"Data Source={_DbPath}")
                                                                    .EnableDetailedErrors(true);
            // We are responsible for the lifetime of the context here.
            using (LineTenDbContext context = new LineTenDbContext(dbContextOptionsBuilder.Options))
                try
                {
                    context.Database.EnsureCreated();
                    DbRepository dbRepo = new Database.DbRepository(context);
                    var dbProduct = dbRepo.Products.Add(newProduct);
                    Assert.True(dbProduct.Id > 0);
                    context.Database.EnsureDeleted();
                }
                catch (Exception ex)
                {
                    context.Database.EnsureDeleted();
                    throw;
                }
        }

        [Fact(DisplayName = "Update Product")]
        public void Update_Product()
        {
            var newProduct = new Product("Prodcut 1", "PRoduct 1 descrtition", "SKU_1");

            DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder()
                                                                    .UseSqlite($"Data Source={_DbPath}")
                                                                    .EnableDetailedErrors(true);
            // We are responsible for the lifetime of the context here.
            using (LineTenDbContext context = new LineTenDbContext(dbContextOptionsBuilder.Options))
                try
                {
                    context.Database.EnsureCreated();
                    DbRepository dbRepo = new Database.DbRepository(context);
                    var dbProduct = dbRepo.Products.Add(newProduct);
                    newProduct = newProduct with { Name = "Salad" };
                    var updatedProduct = dbRepo.Products.Update(newProduct);
                    Assert.True(updatedProduct.Name == newProduct.Name);
                    context.Database.EnsureDeleted();
                }
                catch (Exception ex)
                {
                    context.Database.EnsureDeleted();
                    throw;
                }
        }


        [Fact(DisplayName = "Fetch Product")]
        public void Get_Product()
        {
            var newProduct1 = new Product("Product 1", "PRoduct 1 descrtition", "SKU_1");
            var newProduct2 = new Product("Product 2", "PRoduct 1 descrtition", "SKU_2");
            var newProduct3 = new Product("Product 3", "PRoduct 1 descrtition", "SKU_3");


            DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder()
                                                                    .UseSqlite($"Data Source={_DbPath}")
                                                                    .EnableDetailedErrors(true);
            // We are responsible for the lifetime of the context here.
            using (LineTenDbContext context = new LineTenDbContext(dbContextOptionsBuilder.Options))
                try
                {
                    context.Database.EnsureCreated();
                    DbRepository dbRepo = new Database.DbRepository(context);
                    dbRepo.Products.Add(newProduct1);
                    dbRepo.Products.Add(newProduct2);
                    dbRepo.Products.Add(newProduct3);

                    var Product = dbRepo.Products.Get(3);

                    Assert.True(Product.Name == newProduct3.Name);
                    Assert.True(Product.Id>0);
                }
                catch (Exception ex)
                {
                    context.Database.EnsureDeleted();
                    throw;
                }

        }


        [Fact(DisplayName = "Delete Product")]
        public void Delete_Product()
        {
            var newProduct1 = new Product("Product 1", "PRoduct 1 descrtition", "SKU_1");
            var newProduct2 = new Product("Product 2", "PRoduct 1 descrtition", "SKU_2");
            var newProduct3 = new Product("Product 3", "PRoduct 1 descrtition", "SKU_3");

            DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder()
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
                    dbRepo.Products.Add(newProduct1);
                    dbRepo.Products.Add(newProduct2);
                    dbRepo.Products.Add(newProduct3);

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
                    dbRepo.Products.Delete(3);


                    var excep = Assert.Throws<System.InvalidOperationException>(() =>
                    {
                        dbRepo.Products.Get(3);
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
