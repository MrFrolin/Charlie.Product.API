using Charlie.Product.Shared.DTOs;
using System.Text.Json;

namespace Charlie.Product.API;



public class ProductResponseListener : BackgroundService
{
    private readonly RabbitMqClient _rabbitMqClient;
    private readonly ILogger<ProductResponseListener> _logger;

    public ProductResponseListener(RabbitMqClient rabbitMqClient, ILogger<ProductResponseListener> logger)
    {
        _rabbitMqClient = rabbitMqClient;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _rabbitMqClient.SubscribeAsync("product.responses", async message =>
        {
            try
            {
                var response = JsonSerializer.Deserialize<ProductResponseDTO>(message);

                if (response != null)
                {
                    _logger.LogInformation($"Received response for CorrelationId: {response.CorrelationId}, Status: {response.Status} , Product: {response.Payload.Name}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error processing message: {ex.Message}");
            }
        }, stoppingToken);
    }
}