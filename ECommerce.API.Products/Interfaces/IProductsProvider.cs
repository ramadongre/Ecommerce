using ECommerce.API.Products.DB;

namespace ECommerce.API.Products.Interfaces
{
    public interface IProductsProvider
    {
        Task<(bool IsSuccess, IEnumerable<ECommerce.API.Products.Models.Product>? Products, string? ErrorMessage)> GetProductsAsync();

        Task<(bool IsSuccess, ECommerce.API.Products.Models.Product? Product, string? ErrorMessage)> GetProductAsync(int id);
    }
}
