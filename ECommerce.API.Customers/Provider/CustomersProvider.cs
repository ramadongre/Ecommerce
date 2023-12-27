using AutoMapper;
using ECommerce.API.Customers.DB;
using ECommerce.API.Customers.Interfaces;
using ECommerce.API.Customers.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Customer = ECommerce.API.Customers.Models.Customer;

namespace ECommerce.API.Customers.Provider
{
    public class CustomersProvider : ICustomersProvider
    {
        private readonly CustomersDbContext dbContext;
        private readonly ILogger<CustomersProvider> logger;
        private readonly IMapper mapper;
        public CustomersProvider(CustomersDbContext dbContext, ILogger<CustomersProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }
        private void SeedData()
        {
            if (!dbContext.Customers.Any())
            {
                dbContext.Customers.Add(new DB.Customer() { Id = 1, Name = "John", Address="123 Carter Street Folsom CA" });
                dbContext.Customers.Add(new DB.Customer() { Id = 2, Name = "Gregory", Address = "124 Marsha Hawk Drive Folsom CA" });
                dbContext.Customers.Add(new DB.Customer() { Id = 3, Name = "Ram", Address = "256 Leggins Circle Folsom CA" });
                dbContext.Customers.Add(new DB.Customer() { Id = 4, Name = "Juan", Address = "456 Catrter Street Folsom CA" });
                dbContext.Customers.Add(new DB.Customer() { Id = 5, Name = "Meera", Address = "1237 Catrter Street Folsom CA" });
                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Customer>? Customers, string? ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                var Customers = await dbContext.Customers.ToListAsync();
                if (Customers != null && Customers.Any())
                {
                    var results = mapper.Map<IEnumerable<DB.Customer>, IEnumerable<Models.Customer>>(Customers);
                    return (true, results, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, ECommerce.API.Customers.Models.Customer? Customer, string? ErrorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                var Customer = await dbContext.Customers.FirstOrDefaultAsync(p => p.Id == id);
                if (Customer != null)
                {
                    var result = mapper.Map<DB.Customer, Models.Customer>(Customer);
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
