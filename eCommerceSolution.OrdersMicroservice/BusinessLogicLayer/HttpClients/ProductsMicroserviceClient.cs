using System.Net.Http.Json;
using System.Text.Json;
using BusinessLogicLayer.DTO;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Polly.Bulkhead;

namespace BusinessLogicLayer.HttpClients;

public class ProductsMicroserviceClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ProductsMicroserviceClient> _logger;
    private readonly IDistributedCache _distributedCache;

    public ProductsMicroserviceClient(HttpClient httpClient, ILogger<ProductsMicroserviceClient> logger, IDistributedCache distributedCache)
    {
        _httpClient = httpClient;
        _logger = logger;
        _distributedCache = distributedCache;
    }


    public async Task<ProductDTO?> GetProductByProductID(Guid productID)
    {
        try
        {   //REDIS Cache Save data
            //Key: product:123
            //Value: {"ProductName: ""}

            // Get Product from Redis Cache
            string cacheKey = $"product:{productID}";

            string? cachedProduct = await _distributedCache.GetStringAsync(cacheKey);

            if (cachedProduct != null)
            {
                ProductDTO? productFromCache = JsonSerializer.Deserialize<ProductDTO>(cachedProduct);
                return productFromCache;
            }

            HttpResponseMessage response = await _httpClient.GetAsync($"/api/products/search/product-id/{productID}");

            if (!response.IsSuccessStatusCode)
            {
                // If service is unavailable prevent saving dummy data to redis cache
                if (response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
                {
                    ProductDTO? productResponseFromFallBackPolicy = await response.Content.ReadFromJsonAsync<ProductDTO>();
                    if (productResponseFromFallBackPolicy == null)
                    {
                        throw new NotImplementedException("Fallback policy was not implemented");
                    }
                    return productResponseFromFallBackPolicy;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    throw new HttpRequestException("Bad request", null, System.Net.HttpStatusCode.BadRequest);
                }
                else
                {
                    throw new HttpRequestException($"Http request failed with status code {response.StatusCode}");
                }
            }

            //var resP = await response.Content.ReadAsStringAsync();
            ProductDTO? product = await response.Content.ReadFromJsonAsync<ProductDTO>();

            if (product == null)
            {
                throw new ArgumentException("Invalid Product ID");
            }

            //Save data to Redis Cache
            string productJson = JsonSerializer.Serialize(product);

            var distributedCacheEntryOptions = new DistributedCacheEntryOptions()
                 .SetAbsoluteExpiration(TimeSpan.FromSeconds(300))
                 .SetSlidingExpiration(TimeSpan.FromSeconds(100));

            await _distributedCache.SetStringAsync(cacheKey, productJson, distributedCacheEntryOptions);

            return product;
        }
        catch (BulkheadRejectedException ex)
        {
            _logger.LogError(ex, "Bulkhead isolation blocks the request since the request queue is full");

            return new ProductDTO(
              ProductID: Guid.NewGuid(),
              ProductName: "Temporarily Unavailable (Bulkhead)",
              Category: "Temporarily Unavailable (Bulkhead)",
              UnitPrice: 0,
              QuantityInStock: 0);
        }

    }
}


