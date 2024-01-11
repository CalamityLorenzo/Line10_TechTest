using Domain.Db;
using Domain.Entities;
using Moq;
using unit.Mocks;

namespace unit
{
    public class CustomerDomainRepoTests
    {

        private readonly DbRepoMock dbRepo = new DbRepoMock();
        
        [Fact]
        public void CustomerAdd()
        {
            DomainRepo dm = new DomainRepo(dbRepo);
            var customer = dm.Customers.Add(new("Paul", "Terry", "Phone", "Email"));
            Assert.True(customer.Id == 100);
        }

        [Fact]
        public void CustomerAdd_NoName()
        {
            DomainRepo dm = new DomainRepo(dbRepo);
            Assert.Throws<ArgumentOutOfRangeException>(() => dm.Customers.Add(new(null, "", "Phone", "Email")));
        }

        [Fact]
        public void CustomerAdd_WithId()
        {
            DomainRepo dm = new DomainRepo(dbRepo);
            Assert.Throws<InvalidDataException>(() => dm.Customers.Add(new(50, "Paul", "", "Phone", "Email")));
        }

        [Fact]
        public void CustomerGet_WithBadId()
        {
            DomainRepo dm = new DomainRepo(dbRepo);
            Assert.Throws<ArgumentOutOfRangeException>(() => dm.Customers.Get(-2));
        }

        [Fact]
        public void CustomerGet()
        {
            DomainRepo dm = new DomainRepo(dbRepo);
            var customer = dm.Customers.Get(100);
            Assert.True((int)customer.Id == 100);

        }

        [Fact]
        public void CustomerDelete()
        {
            DomainRepo dm = new DomainRepo(dbRepo);
            var Customer = dm.Customers.Get(100);
            dm.Customers.Delete(Customer);
            Assert.True(true);

        }
        [Fact]
        public void CustomerDelete_BadId()
        {
            DomainRepo dm = new DomainRepo(dbRepo);
            var Customer = dm.Customers.Get(100);
           Assert.Throws< ArgumentOutOfRangeException>(()=> { dm.Customers.Delete(Customer with { Id = 0 }); });
            Assert.True(true);

        }
    }
}