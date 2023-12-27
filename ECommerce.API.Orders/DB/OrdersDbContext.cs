﻿using ECommerce.API.Orders.DB;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Orders.DB
{
    public class OrdersDbContext : DbContext
    {
        public DbSet<Order>? Orders { get; set; }

        public OrdersDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
