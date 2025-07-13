using System.Text;
using Microsoft.Extensions.Logging;
using Polly.Fallback;
using Polly;
using BusinessLogicLayer.DTO;
using System.Text.Json;
using Polly.Bulkhead;

namespace BusinessLogicLayer.Policies;

public class ProductsMicroservicePolicies : IProductsMicroservicePolicies
{
    private readonly ILogger<ProductsMicroservicePolicies> _logger;

    public ProductsMicroservicePolicies(ILogger<ProductsMicroservicePolicies> logger)
    {
        _logger = logger;
    }


    public IAsyncPolicy<HttpResponseMessage> GetFallbackPolicy()
    {

        AsyncFallbackPolicy<HttpResponseMessage> policy = Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
          .FallbackAsync(async (context) =>
          {
              _logger.LogWarning("Fallback triggered: The request failed, returning dummy data");

            ProductDTO product = new ProductDTO(ProductID: Guid.Empty,
            ProductName: "Temporarily Unavailable (fallback)",
            Category: 0,
            UnitPrice: 0,
            QuantityInStock: 0
            );

              var response = new HttpResponseMessage(System.Net.HttpStatusCode.ServiceUnavailable)
              {
                  Content = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json")
              };
              await Task.Delay(0);
              return response;
          });

        return policy;
    }

    public IAsyncPolicy<HttpResponseMessage> GetBulkheadIsolationPolicy()
    {
        AsyncBulkheadPolicy<HttpResponseMessage> policy = Policy.BulkheadAsync<HttpResponseMessage>(
          maxParallelization: 2, //Allows up to 2 concurrent requests
          maxQueuingActions: 40, //Queue up to 40 additional requests
          onBulkheadRejectedAsync: (context) => {
              _logger.LogWarning("BulkheadIsolation triggered. Can't send any more requests since the queue is full");

              throw new BulkheadRejectedException("Bulkhead queue is full");
          }
          );

        return policy;
    }
}

