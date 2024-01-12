using Domain.Entities;
using integration.Mocks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;

namespace integration.Web
{
    public class CustomerApiTests
    {
        private DbRepoMock _mockDb;
        private DomainRepo _domainRepo;
        private CustomersController _CustomersController;

        public CustomerApiTests()
        {
            _mockDb = new DbRepoMock();
            _domainRepo = new DomainRepo(_mockDb);
            _CustomersController = new CustomersController(_domainRepo);
        }

        [Fact]
        public void Customer_Get()
        {
            var Customer = _CustomersController.Get(100);
            Assert.True(Customer.Value != null);
            Assert.True(Customer.Value.Id == 100);
        }

        [Fact]
        public void Customer_Get_BadId()
        {
            var Customer = _CustomersController.Get(-1);
            Assert.True(Customer.Result is StatusCodeResult);
            Assert.True((Customer.Result as StatusCodeResult).StatusCode == 404);
        }

        [Fact]
        public void Customer_Add()
        {
            Customer badCustomer = new ("Paul", "Get", "1", "2@mail.com");

            var Customer = _CustomersController.Post(badCustomer);
            Assert.True(Customer.Value.Id == 100);
        }

        [Fact]
        public void Customer_Add_BadId()
        {
            Customer badCustomer = new (100, "Paul", "Get", "1", "2@mail.com");

            var Customer = _CustomersController.Post(badCustomer);
            Assert.True(Customer.Result is ObjectResult);
            Assert.True((Customer.Result as ObjectResult).StatusCode == 500);

        }

        [Fact]
        public void Customer_Update()
        {
            Customer customer = new(100, "Paul", "UpdatedName", "1", "2@mail.com");

            var Customer = _CustomersController.Update(customer);
            Assert.True(Customer.Value.Id == 100 && Customer.Value.LastName== "UpdatedName");
        }

        [Fact]
        public void Customer_Update_BadId()
        {
            Customer badCustomer = new("Paul", "UpdatedName", "1", "2@mail.com");

            var Customer = _CustomersController.Update(badCustomer);
            Assert.True(Customer.Result is StatusCodeResult);
            Assert.True((Customer.Result as StatusCodeResult).StatusCode == 404);

        }

        [Fact]
        public void Customer_Delete()
        {
            var Customer = _CustomersController.Delete(100);
            Assert.True(Customer is StatusCodeResult);
            Assert.True((Customer as StatusCodeResult).StatusCode == 200);
        }

        [Fact]
        public void Customer_Delete_BadId()
        {

            var Customer = _CustomersController.Delete(-5);

            Assert.True(Customer is StatusCodeResult);
            Assert.True((Customer as StatusCodeResult).StatusCode == 404);

        }
    }
}
