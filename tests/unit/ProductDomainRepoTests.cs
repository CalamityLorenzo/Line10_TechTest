using Domain.Entities;
using unit.Mocks;

namespace unit
{
    public class ProductDomainRepoTests
    {

        private readonly DbRepoMock dbRepo = new DbRepoMock();

        [Fact]
        public void ProductAdd()
        {
            DomainRepo dm = new DomainRepo(dbRepo);
            var Product = dm.Products.Add(new("Prod01", "Prod01 Description", "SKU 01"));
            Assert.True(Product.Id == 100);
        }

        [Fact]
        public void ProductAdd_NoName()
        {
            DomainRepo dm = new DomainRepo(dbRepo);
            Assert.Throws<ArgumentOutOfRangeException>(() => dm.Products.Add(new(null, "Prod01 Description", "SKU 01")));
        }

        [Fact]
        public void ProductAdd_NoDescription()
        {
            DomainRepo dm = new DomainRepo(dbRepo);
            Assert.Throws<ArgumentOutOfRangeException>(() => dm.Products.Add(new("Prod01", "", "SKU 01")));
        }

        [Fact]
        public void ProductAdd_WithId()
        {
            DomainRepo dm = new DomainRepo(dbRepo);
            Assert.Throws<InvalidDataException>(() => dm.Products.Add(new(50, "Prod01", "Prod01 Description", "SKU 01")));
        }

        [Fact]
        public void ProductGet_WithBadId()
        {
            DomainRepo dm = new DomainRepo(dbRepo);
            Assert.Throws<ArgumentOutOfRangeException>(() => dm.Products.Get(-2));
        }

        [Fact]
        public void ProductGet()
        {
            DomainRepo dm = new DomainRepo(dbRepo);
            var Product = dm.Products.Get(100);
            Assert.True((int)Product.Id == 100);

        }

        [Fact]
        public void ProductUpdate()
        {
            DomainRepo dm = new DomainRepo(dbRepo);
            var Product = dm.Products.Get(100);
            var updatedProduct = dm.Products.Update(Product with { Description = "Updated Product DEscription" });
            Assert.True((int)updatedProduct.Id == 100);
            Assert.True(updatedProduct .Description == "Updated Product DEscription");

        }

        [Fact]
        public void ProductUpdate_BadId()
        {
            DomainRepo dm = new DomainRepo(dbRepo);
            var Product = dm.Products.Get(100);

            Assert.Throws<ArgumentOutOfRangeException>(() => { dm.Products.Update(Product with { Id = 0, Description = "Updated Product DEscription" }); });
            

        }

        [Fact]
        public void ProductDelete()
        {
            DomainRepo dm = new DomainRepo(dbRepo);
            var Product = dm.Products.Get(100);
            dm.Products.Delete(Product);
            Assert.True(true);
            
        }
        [Fact]
        public void ProductDelete_BadId()
        {
            DomainRepo dm = new DomainRepo(dbRepo);
            var Product = dm.Products.Get(100);
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                dm.Products.Delete(Product with { Id = 0 });
            });
            Assert.True(true);

        }
    }
}
