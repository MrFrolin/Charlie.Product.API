using Charlie.Product.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Charlie.Product.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly RabbitMqClient _rabbitMqClient;

        public ProductController(RabbitMqClient rabbitMqClient)
        {
            _rabbitMqClient = rabbitMqClient;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok("products");
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProductByIdAsync(int id)
        {
            if (id <= 0 || id == null)
            {
                return BadRequest("Invalid product ID.");
            }
            var correlationId = Guid.NewGuid().ToString();
            var message = new
            {
                CorrelationId = correlationId,
                Operation = "Read",
                Payload = new { Id = id}
            };
            await _rabbitMqClient.PublishAsync("product.operations", message);
            return Accepted(new { Message = "Product retrieval started.", CorrelationId = correlationId});
        }

        [HttpPost]
        public async Task<IActionResult> AddProductAsync([FromBody] ProductDTO productDTO)
        {
            if (productDTO.Id == null)
            {
                return BadRequest();
            }

            var correlationId = Guid.NewGuid().ToString();

            var message = new
            {
                CorrelationId = correlationId,
                Operation = "Create",
                Payload = productDTO
            };

            await _rabbitMqClient.PublishAsync("product.operations", message);
            return Accepted(new { Message = "Product creation started.", CorrelationId = correlationId, message.Payload});
        }
    }
}