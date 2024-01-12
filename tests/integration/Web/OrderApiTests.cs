using Domain.Db;
using Domain.Entities;
using integration.Mocks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;

namespace integration.Web
{
    public class OrderApiTests
    {
        private IDbRepository _mockDb;
        private DomainRepo _domainRepo;
        private OrdersController _OrdersController;

        public OrderApiTests()
        {
            _mockDb = new DbRepoMock();
            _domainRepo = new DomainRepo(_mockDb);
            _OrdersController = new OrdersController(_domainRepo);
        }

        [Fact]
        public void Order_Get()
        {
            var Order = _OrdersController.Get(10, 200);
            Assert.True(Order.Value != null);
            Assert.True(Order.Value.CustomerId == 10);
        }

        [Fact]
        public void Order_Get_BadCustomerId()
        {
            var Order = _OrdersController.Get(-1, 200);
            Assert.True(Order.Result is StatusCodeResult);
            Assert.True((Order.Result as StatusCodeResult).StatusCode == 404);
        }

        public void Order_Get_BadProductId()
        {
            var Order = _OrdersController.Get(-1, 200);
            Assert.True(Order.Result is StatusCodeResult);
            Assert.True((Order.Result as StatusCodeResult).StatusCode == 404);
        }

        [Fact]
        public void Order_Add()
        {
            Order order = new(10, 20, "Green", DateTimeOffset.Parse("01/01/2001"), DateTimeOffset.Parse("01/01/2002"));

            var newOrder = _OrdersController.Post(order);
            Assert.True(newOrder.Value.CustomerId == 10);
        }

        [Fact]
        public void Order_Add_BadCustomerId()
        {
            Order badOrder = new(10, 0, "Green", DateTimeOffset.Parse("01/01/2001"), DateTimeOffset.Parse("01/01/2002"));

            var Order = _OrdersController.Post(badOrder);
            Assert.True(Order.Result is ObjectResult);
            Assert.True((Order.Result as ObjectResult).StatusCode == 500);
        }

        public void Order_Add_BadProductId()
        {
            Order badOrder = new(-6, 10, "Green", DateTimeOffset.Parse("01/01/2001"), DateTimeOffset.Parse("01/01/2002"));

            var Order = _OrdersController.Post(badOrder);
            Assert.True(Order.Result is StatusCodeResult);
            Assert.True((Order.Result as StatusCodeResult).StatusCode == 500);

        }

        [Fact]
        public void Order_Update()
        {
            Order order = new(10, 20, "Status UPDATED", DateTimeOffset.Parse("01/01/2001"), DateTimeOffset.Parse("01/01/2002"));

            var orderUpdate = _OrdersController.Update(order);
            Assert.True(orderUpdate.Value.ProductId == 10 && orderUpdate.Value.Status == "Status UPDATED");
        }

        [Fact]
        public void Order_Update_BadProductId()
        {
            Order badOrder = new(-6, 10, "Green", DateTimeOffset.Parse("01/01/2001"), DateTimeOffset.Parse("01/01/2002"));


            var Order = _OrdersController.Update(badOrder);
            Assert.True(Order.Result is ObjectResult);

            Assert.True((Order.Result as ObjectResult).StatusCode == 500);
        }

        [Fact]
        public void Order_Update_BadCustomerId()
        {
            Order badOrder = new(10, 0, "Green", DateTimeOffset.Parse("01/01/2001"), DateTimeOffset.Parse("01/01/2002"));
            var Order = _OrdersController.Update(badOrder);
            Assert.True(Order.Result is ObjectResult);

            Assert.True((Order.Result as ObjectResult).StatusCode == 500);
        }

        [Fact]
        public void Order_Delete()
        {
            var Order = _OrdersController.Delete(10, 20);
            Assert.True(Order is StatusCodeResult);
            Assert.True((Order as StatusCodeResult).StatusCode == 200);
        }

        [Fact]
        public void Order_Delete_BadProductId()
        {

            var Order = _OrdersController.Delete(10, 0);

            Assert.True(Order is StatusCodeResult);
            Assert.True((Order as StatusCodeResult).StatusCode == 404);

        }

        [Fact]
        public void Order_Delete_BadCustomerId()
        {

            var Order = _OrdersController.Delete(0, 10);

            Assert.True(Order is StatusCodeResult);
            Assert.True((Order as StatusCodeResult).StatusCode == 404);

        }
    }
}
