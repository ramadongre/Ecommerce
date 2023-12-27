using ECommerce.API.Search.Interfaces;
using ECommerce.API.Search.Models;
using System.Text.Json;

namespace ECommerce.API.Search.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IHttpClientFactory httpClientfactory;
        private ILogger<CustomerService> logger;

        public CustomerService(IHttpClientFactory _httpClientFactory, ILogger<CustomerService> logger)
        {
            this.httpClientfactory = _httpClientFactory;
            this.logger = logger;
        }
        async Task<(bool IsSuccess, Customer? customer, string? ErrorMessage)> ICustomerService.GetCustomerAsync(int customerId)
        {
            try
            {
                var client = httpClientfactory.CreateClient("CustomersService");
                var response = await client.GetAsync($"api/Customers/{customerId}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true,
                    };
                    var result = JsonSerializer.Deserialize<Customer>(content, options);

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
