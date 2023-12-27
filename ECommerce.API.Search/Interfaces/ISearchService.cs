using System.Dynamic;
namespace ECommerce.API.Search.Interfaces
{
    public interface ISearchService
    {
        Task<(bool IsSuccess, dynamic? SearchResults)> SearchAsync(int customerId);
    }
}
