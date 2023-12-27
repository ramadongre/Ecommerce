using ECommerce.API.Customers.DB;

namespace ECommerce.API.Customers.Interfaces
{
    public interface ICustomersProvider
    {
        Task<(bool IsSuccess, IEnumerable<ECommerce.API.Customers.Models.Customer>? Customers, string? ErrorMessage)> GetCustomersAsync();

        Task<(bool IsSuccess, ECommerce.API.Customers.Models.Customer? Customer, string? ErrorMessage)> GetCustomerAsync(int id);
    }
}
