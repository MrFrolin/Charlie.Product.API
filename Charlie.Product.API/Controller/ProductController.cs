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
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProductByIdAsync(int productId)
        {
            if (productId == null)
            {
                return BadRequest();
            }
            var correlationId = Guid.NewGuid().ToString();
            var message = new
            {
                CorrelationId = correlationId,
                ProductId = productId
            };
            await _rabbitMqClient.PublishAsync("product.operations", message);
            return Accepted(new { Message = "Product retrieval started.", CorrelationId = correlationId });
        }

        [HttpPost]
        public async Task<IActionResult> AddProductAsync([FromBody] ProductDTO productDTO)
        {
            if (productDTO == null)
            {
                return BadRequest();
            }

            var correlationId = Guid.NewGuid().ToString();

            var message = new
            {
                CorrelationId = correlationId,
                ProductId = productDTO.ProductId,
                Name = productDTO.Name,
                Price = productDTO.Price
            };

            await _rabbitMqClient.PublishAsync("product.operations", message);
            return Accepted(new { Message = "Product creation started.", CorrelationId = correlationId });
        }
    }
}