using ECommerce.API.Customers.DB;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Customers.DB
{
    public class CustomersDbContext : DbContext
    {
        public DbSet<Customer>? Customers { get; set; }

        public CustomersDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
