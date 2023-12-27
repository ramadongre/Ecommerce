using ECommerce.API.Search.Models;
using System.Dynamic;
namespace ECommerce.API.Search.Interfaces
{
    public interface ICustomerService
    {
        Task<(bool IsSuccess, Customer? customer, string? ErrorMessage)> GetCustomerAsync(int customerId);
    }
}
