using ECommerce.API.Search.Interfaces;
using ECommerce.API.Search.Models;
using System.Text.Json;

namespace ECommerce.API.Search.Services
{
    public class OrderService : IOrderService
    {
        private readonly IHttpClientFactory httpClientfactory;
        private ILogger<OrderService> logger;

        public OrderService(IHttpClientFactory _httpClientFactory, ILogger<OrderService> logger)
        {
            this.httpClientfactory = _httpClientFactory;
            this.logger = logger;
        }
        async Task<(bool isSuccess, IEnumerable<Order>? Orders, string? ErrorMessage)> IOrderService.GetOrderAsync(int customerid)
        {
            try
            {
                var client = httpClientfactory.CreateClient("OrderService");
                var response = await client.GetAsync($"api/orders/{customerid}");
                if (response.IsSuccessStatusCode)
                { 
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true,
                    };
                    var result = JsonSerializer.Deserialize<IEnumerable<Order>>(content,options);

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
