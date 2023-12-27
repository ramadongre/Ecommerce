using ECommerce.API.Search.Interfaces;
using ECommerce.API.Search.Models;
using System.Text.Json;

namespace ECommerce.API.Search.Services
{
    public class ProductsService : IProductService
    {
        private readonly IHttpClientFactory httpClientfactory;
        private ILogger<ProductsService> logger;

        public ProductsService(IHttpClientFactory _httpClientFactory, ILogger<ProductsService> logger)
        {
            this.httpClientfactory = _httpClientFactory;
            this.logger = logger;
        }
        async Task<(bool IsSuccess, IEnumerable<Product>? Products, string? ErrorMessage)> IProductService.GetProductsAsync()
        {
            try
            {
                var client = httpClientfactory.CreateClient("ProductsService");
                var response = await client.GetAsync($"api/products");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true,
                    };
                    var result = JsonSerializer.Deserialize<IEnumerable<Product>>(content, options);

                    return (true, result, null);
                }
                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }       
    }
}
