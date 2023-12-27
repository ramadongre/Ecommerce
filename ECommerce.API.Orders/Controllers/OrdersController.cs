using ECommerce.API.Orders.Interfaces;
using ECommerce.API.Orders.Provider;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Orders.Controllers
{
    [ApiController]
    [Route("api/Orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersProvider _OrdersProvider;
        public OrdersController(IOrdersProvider OrdersProvider)
        {
            _OrdersProvider = OrdersProvider;
        }

        [HttpGet("{CustomerId}")]
        public async Task<IActionResult> GetOrdersAsync(int CustomerId)
        {
            var result = await _OrdersProvider.GetOrdersAsync(CustomerId);
            if (result.IsSuccess)
            {
                return Ok(result.Orders);
            }

            return NotFound();
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetOrderAsync(int id)
        //{
        //    var result = await _OrdersProvider.GetOrderAsync(id);
        //    if (result.IsSuccess)
        //    {
        //        return Ok(result.Order);
        //    }

        //    return NotFound();
        //}

    }
}
