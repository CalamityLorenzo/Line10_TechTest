using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly DomainRepo domainRepo;
        public OrdersController(DomainRepo domainRepo)
        {
            this.domainRepo = domainRepo;
        }


        // GET api/<OrdersController>/5
        [HttpGet("{customerId}/{productId}")]
        public ActionResult<Order> Get(int customerId, int productId)
        {
            try
            {
                return domainRepo.Orders.Get(customerId, productId);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return StatusCode(404);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<OrdersController>
        [HttpPost("add")]
        public ActionResult<Order> Post([FromBody] Order order)
        {
            try
            {
                return domainRepo.Orders.Add(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<OrdersController>/5
        [HttpPost("Update")]
        public ActionResult<Order> Update([FromBody] Order order)
        {
            try
            {
                return domainRepo.Orders.Update(order);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return StatusCode(404);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<OrdersController>/5

        [HttpPost("delete/{customerid}/{productId}")]
        public ActionResult Delete(int customerid, int productId)
        {
            try
            {

                var customer = domainRepo.Orders.Get(customerid, productId);
                domainRepo.Orders.Delete(customer);
                return StatusCode(200);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return StatusCode(404);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
