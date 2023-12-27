using ECommerce.API.Search.Models;

namespace ECommerce.API.Search.Interfaces
{
    public interface IOrderService
    {
        public Task<(bool isSuccess, IEnumerable<Order>? Orders, string? ErrorMessage)> GetOrderAsync(int customerid);
    }
}
