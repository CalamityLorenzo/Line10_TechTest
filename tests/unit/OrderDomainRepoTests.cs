using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using unit.Mocks;

namespace unit
{
    public class OrderDomainRepoTests
    {
        private readonly DbRepoMock dbRepo = new DbRepoMock();

        [Fact]
        public void OrderAdd()
        {
            DomainRepo dm = new DomainRepo(dbRepo);
            var Order = dm.Orders.Add(new(10, 20, "Green", DateTimeOffset.Parse("01/01/2001"), DateTimeOffset.Parse("01/01/2002")));
            Assert.True(Order.CustomerId == 10);
            Assert.True(Order.ProductId == 20);
        }

        [Fact]
        public void OrderAdd_BadCustomer()
        {
            DomainRepo dm = new DomainRepo(dbRepo);
            Assert.Throws<InvalidDataException>(() => dm.Orders.Add(new(20, 0, "Green", DateTimeOffset.Parse("01/01/2001"), DateTimeOffset.Parse("01/01/2002"))));
        }
        [Fact]
        public void OrderAdd_BadProduct()
        {
            DomainRepo dm = new DomainRepo(dbRepo);
            Assert.Throws<InvalidDataException>(() => dm.Orders.Add(new(-5, 10, "Green", DateTimeOffset.Parse("01/01/2001"), DateTimeOffset.Parse("01/01/2002"))));
        }

        [Fact]
        public void OrderGet()
        {
            DomainRepo dm = new DomainRepo(dbRepo);
            var Order = dm.Orders.Get(20, 10);
            Assert.True((int)Order.CustomerId == 20);
            Assert.True((int)Order.ProductId == 10);

        }

        [Fact]
        public void OrderUpdate()
        {
            DomainRepo dm = new DomainRepo(dbRepo);
            var Order = dm.Orders.Update(new(10, 20, "Status UPDATED", DateTimeOffset.Parse("01/01/2001"), DateTimeOffset.Parse("01/01/2002")));
            Assert.True(Order.Status == "Status UPDATED");
        }

        [Fact]
        public void OrderDelete()
        {
            DomainRepo dm = new DomainRepo(dbRepo);
            var order = dm.Orders.Get(10, 20);
            dm.Orders.Delete(order);
        }
    }
}
