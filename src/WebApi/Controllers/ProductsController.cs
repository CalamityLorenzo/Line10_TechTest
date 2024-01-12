using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly DomainRepo domainRepo;

        public ProductsController(DomainRepo domainRepo)
        {
            this.domainRepo = domainRepo;
        }

        [HttpGet("{productId}")]
        public ActionResult<Product> Get(int productId)
        {
            try
            {
                return domainRepo.Products.Get(productId);
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
        public ActionResult<Product> Post([FromBody] Product Product)
        {
            try
            {
                return domainRepo.Products.Add(Product);
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
        public ActionResult<Product> Update([FromBody] Product Product)
        {
            try
            {
                return domainRepo.Products.Update(Product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("delete/{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var Product = domainRepo.Products.Get(id);
                domainRepo.Products.Delete(Product);
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
