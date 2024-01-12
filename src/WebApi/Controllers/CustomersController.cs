using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly DomainRepo domainRepo;

        CustomersController(DomainRepo domainRepo)
        {
            this.domainRepo = domainRepo;
        }

        [HttpGet("{customerId}")]
        public ActionResult<Customer> Get(int customerId)
        {
            try
            {
                return domainRepo.Customers.Get(customerId);
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

        [HttpPost("add")]
        public ActionResult<Customer> Post([FromBody] Customer customer)
        {
            try
            {
                return domainRepo.Customers.Add(customer);
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

        [HttpPost("update")]
        public ActionResult<Customer> Update([FromBody] Customer customer)
        {
            try
            {
                return domainRepo.Customers.Update(customer);
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

        [HttpPost("delete/{customerId}")]
        public ActionResult Delete(int customerId)
        {
            try { 
            
                var customer = domainRepo.Customers.Get(customerId);
                 domainRepo.Customers.Delete(customer);
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
