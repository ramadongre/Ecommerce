using ECommerce.API.Search.Interfaces;

namespace ECommerce.API.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;
        public SearchService(IOrderService orderService, IProductService productService, ICustomerService customerService)
        {
            this._orderService = orderService;
            _productService = productService;
            _customerService = customerService;
        }
        public async Task<(bool IsSuccess, dynamic? SearchResults)> SearchAsync(int customerId)
        {
            var orderResult = await _orderService.GetOrderAsync(customerId);
            var productresult = await _productService.GetProductsAsync();
            var customerresult = await _customerService.GetCustomerAsync(customerId);

            if (orderResult.isSuccess)
            {
                foreach (var order in orderResult.Orders)
                {
                    if (customerresult.IsSuccess && customerresult.customer != null)
                    {
                        order.CustomerId = customerId;
                        order.CustomerName = customerresult.customer.Name;
                        order.CustomerAddress = customerresult.customer.Address;
                    }

                    foreach (var item in order.Items)
                    {
                        item.Productname = productresult.IsSuccess ? productresult.Products.FirstOrDefault(p => p.Id == item.ProductId)?.Name
                            : "Product information not available";
                    }
                }

                var result = new
                {
                    Orders = orderResult.Orders
                };

                return (true, result);
            }
            return (false, null);
        }
    }
}
