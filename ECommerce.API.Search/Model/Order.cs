using Microsoft.AspNetCore.Http.Features;

namespace ECommerce.API.Search.Models
{
    public class Order
    {
        public int Id { get; set; }        
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
        public List<OrderItem>? Items { get; set; }
    }
}
