using ECommerce.API.Search.Interfaces;
using ECommerce.API.Search.Model;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Search.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService searchService;
        public SearchController(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        [HttpPost]
        public async Task<IActionResult> SearchAsync(SearchTerm Term)
        {
            var result = await searchService.SearchAsync(Term.CustomerID);
            if (result.IsSuccess)
            {
                return Ok(result.SearchResults);
            }
            return NotFound();
        }
    }
}
