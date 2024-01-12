using Domain.Entities;
using integration.Mocks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;

namespace integration.Web
{
    public class ProductApiTests
    {
        private DbRepoMock _mockDb;
        private DomainRepo _domainRepo;
        private ProductsController _productsController;

        public ProductApiTests()
        {
            _mockDb = new DbRepoMock();
            _domainRepo = new DomainRepo(_mockDb);
            _productsController = new ProductsController(_domainRepo);
        }

        [Fact]
        public void Product_Get()
        {
            var product = _productsController.Get(100);
            Assert.True(product.Value != null);
            Assert.True(product.Value.Id == 100);
        }

        [Fact]
        public void Product_Get_BadId()
        {
            var product = _productsController.Get(-1);
            Assert.True(product.Result is StatusCodeResult);
            Assert.True((product.Result as StatusCodeResult).StatusCode == 404);
        }
    
        [Fact]
        public void Product_Add()
        {
            Product badProduct = new("Prod01", "Prod01 Description", "SKU 01");

            var product = _productsController.Post(badProduct);
            Assert.True(product.Value.Id == 100);
        }

        [Fact]
        public void Product_Add_BadId()
        {
            Product badProduct = new(50, "Prod01", "Prod01 Description", "SKU 01");

            var product = _productsController.Post(badProduct);
            Assert.True(product.Result is ObjectResult);
            Assert.True((product.Result as ObjectResult).StatusCode == 500);

        }

        [Fact]
        public void Product_Update()
        {
            Product badProduct = new(100, "ProdUpdate", "Prod01 Description", "SKU 01");

            var product = _productsController.Update(badProduct);
            Assert.True(product.Value.Id == 100 && product.Value.Name == "ProdUpdate");
        }

        [Fact]
        public void Product_Update_BadId()
        {
            Product badProduct = new("Prod01", "Prod01 Description", "SKU 01");

            var product = _productsController.Update(badProduct);
            Assert.True(product.Result is ObjectResult);
            Assert.True((product.Result as ObjectResult).StatusCode == 500);

        }

        [Fact]
        public void Product_Delete()
        {
            var product = _productsController.Delete(100);
            Assert.True(product is StatusCodeResult);
            Assert.True((product as StatusCodeResult).StatusCode == 200);
        }

        [Fact]
        public void Product_Delete_BadId()
        {

            var product = _productsController.Delete(-5);

            Assert.True(product is StatusCodeResult);
            Assert.True((product as StatusCodeResult).StatusCode == 404);

        }

    }
}
