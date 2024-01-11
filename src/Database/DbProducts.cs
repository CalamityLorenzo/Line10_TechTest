using Database.DbEntities;
using Domain.Db;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    internal class DbProducts(LineTenDbContext context) : IDbProducts
    {

        ProductDb ToDb(Product Product) => new DbEntities.ProductDb
        {
            Id = Product.Id,
            Name = Product.Name,
            Description = Product.Description,
            SKU = Product.SKU
        };

        Product ToClient(ProductDb Product) => new Product(
            Id: Product.Id,
           Name: Product.Name,
           Description: Product.Description,
           SKU: Product.SKU);


        public Product Add(Product Product)
        {
            var result = context.Products.Add(ToDb(Product));

            context.SaveChanges();
            // Never know if this is just cheeky.
            return Product with { Id = result.Entity.Id };
        }

        public void Delete(Product Product)
        {
            context.Remove(ToDb(Product));
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var Product = Get(id);
            Delete(Product);
        }

        public Product Get(int id)
        {
            var ProductDb = context.Products.AsNoTracking().First(a => a.Id == id);
            return ToClient(ProductDb);
        }


        public Product Update(Product Product)
        {
            var result = context.Products.Update(ToDb(Product));
            return ToClient(result.Entity);
        }
    }
}