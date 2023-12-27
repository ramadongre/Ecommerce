using AutoMapper;
using ECommerce.API.Orders.DB;
using ECommerce.API.Orders.Interfaces;
using ECommerce.API.Orders.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Order = ECommerce.API.Orders.Models.Order;
using OrderItem = ECommerce.API.Orders.DB.OrderItem;

namespace ECommerce.API.Orders.Provider
{
    public class OrdersProvider : IOrdersProvider
    {
        private readonly OrdersDbContext dbContext;
        private readonly ILogger<OrdersProvider> logger;
        private readonly IMapper mapper;
        public OrdersProvider(OrdersDbContext dbContext, ILogger<OrdersProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }
        private void SeedData()
        {
            if (!dbContext.Orders.Any())
            {
                //List<OrderItem> orderItems = new List<OrderItem>();
                //orderItems.Add(new OrderItem() { Id = 1, OrderId = 1, ProductId = 1, Quantity = 5, UnitPrice = 2 });
                //orderItems.Add(new OrderItem() { Id = 2, OrderId = 1, ProductId = 2, Quantity = 10, UnitPrice = 21 });
                //orderItems.Add(new OrderItem() { Id = 3, OrderId = 1, ProductId = 3, Quantity = 7, UnitPrice = 44 });
                //orderItems.Add(new OrderItem() { Id = 4, OrderId = 1, ProductId = 4, Quantity = 1, UnitPrice = 17 });
                //orderItems.Add(new OrderItem() { Id = 5, OrderId = 1, ProductId = 5, Quantity = 4, UnitPrice = 3 });
                //dbContext.Orders.Add(new DB.Order() { Id = 1, CustomerId = 1, OrderDate = Convert.ToDateTime("2023-10-01"), Items = orderItems, Total = 100 });

                //List<OrderItem> orderItems1 = new List<OrderItem>();
                //orderItems1.Add(new OrderItem() { Id = 11, OrderId = 2, ProductId = 11, Quantity = 15, UnitPrice = 12 });
                //orderItems1.Add(new OrderItem() { Id = 12, OrderId = 2, ProductId = 12, Quantity = 110, UnitPrice = 121 });
                //orderItems1.Add(new OrderItem() { Id = 13, OrderId = 2, ProductId = 13, Quantity = 17, UnitPrice = 144 });
                //orderItems1.Add(new OrderItem() { Id = 14, OrderId = 2, ProductId = 14, Quantity = 11, UnitPrice = 117 });
                //orderItems1.Add(new OrderItem() { Id = 15, OrderId = 2, ProductId = 15, Quantity = 14, UnitPrice = 13 });
                //dbContext.Orders.Add(new DB.Order() { Id = 2, CustomerId = 2, OrderDate = Convert.ToDateTime("2023-11-01"), Items = orderItems1, Total = 100 });

                dbContext.Orders.Add(new DB.Order()
                {
                    Id = 1,
                    CustomerId = 1,
                    OrderDate = DateTime.Now,
                    Items = new List<OrderItem>()
                    {
                        new OrderItem() { OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 1, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 1, ProductId = 3, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 2, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice = 100 }
                    },
                    Total = 100
                });
                dbContext.Orders.Add(new DB.Order()
                {
                    Id = 2,
                    CustomerId = 1,
                    OrderDate = DateTime.Now.AddDays(-1),
                    Items = new List<OrderItem>()
                    {
                        new OrderItem() { OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 1, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 1, ProductId = 3, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 2, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice = 100 }
                    },
                    Total = 100
                });
                dbContext.Orders.Add(new DB.Order()
                {
                    Id = 3,
                    CustomerId = 2,
                    OrderDate = DateTime.Now,
                    Items = new List<OrderItem>()
                    {
                        new OrderItem() { OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 2, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice = 100 }
                    },
                    Total = 100
                });
                dbContext.SaveChanges();


            }
        }

        //public async Task<(bool IsSuccess, IEnumerable<Models.Order>? Orders, string? ErrorMessage)> GetOrdersAsync(int CustomerId)
        //{
        //    try
        //    {
        //        var Orders = await dbContext.Orders.ToListAsync();
        //        if (Orders != null && Orders.Any())
        //        {
        //            var results = mapper.Map<IEnumerable<DB.Order>, IEnumerable<Models.Order>>(Orders);
        //            return (true, results, null);
        //        }
        //        return (false, null, "Not Found");
        //    }
        //    catch (Exception ex)
        //    {
        //        logger?.LogError(ex.ToString());
        //        return (false, null, ex.Message);
        //    }
        //}

        public async Task<(bool IsSuccess, IEnumerable<ECommerce.API.Orders.Models.Order>? Orders, string? ErrorMessage)> GetOrdersAsync(int CustomerId)
        {
            try
            {
                var Orders = await dbContext.Orders.Where(p => p.CustomerId == CustomerId).Include(p => p.Items).ToListAsync();
                if (Orders != null && Orders.Any())
                {
                    var result = mapper.Map<IEnumerable<DB.Order>, IEnumerable<Models.Order>>(Orders);
                    return (true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
