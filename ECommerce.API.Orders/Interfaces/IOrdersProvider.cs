using ECommerce.API.Orders.DB;

namespace ECommerce.API.Orders.Interfaces
{
    public interface IOrdersProvider
    {
        Task<(bool IsSuccess, IEnumerable<ECommerce.API.Orders.Models.Order>? Orders, string? ErrorMessage)> GetOrdersAsync(int CustomerId);

        //Task<(bool IsSuccess, ECommerce.API.Orders.Models.Order? Order, string? ErrorMessage)> GetOrderAsync(int id);
    }
}
