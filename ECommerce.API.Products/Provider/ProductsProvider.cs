using AutoMapper;
using ECommerce.API.Products.DB;
using ECommerce.API.Products.Interfaces;
using ECommerce.API.Products.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Product = ECommerce.API.Products.Models.Product;

namespace ECommerce.API.Products.Provider
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly ProductsDbContext dbContext;
        private readonly ILogger<ProductsProvider> logger;
        private readonly IMapper mapper;
        public ProductsProvider(ProductsDbContext dbContext, ILogger<ProductsProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }
        private void SeedData()
        {
            if (!dbContext.Products.Any())
            {
                dbContext.Products.Add(new DB.Product() { Id = 1, Name = "Keyboard", Inventory = 20, Price = 15 });
                dbContext.Products.Add(new DB.Product() { Id = 2, Name = "Mice", Inventory = 10, Price = 10 });
                dbContext.Products.Add(new DB.Product() { Id = 3, Name = "Monitor", Inventory = 40, Price = 105 });
                dbContext.Products.Add(new DB.Product() { Id = 4, Name = "Harddisk", Inventory = 50, Price = 205 });
                dbContext.Products.Add(new DB.Product() { Id = 5, Name = "UPS", Inventory = 60, Price = 150 });
                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Product>? Products, string? ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var Products = await dbContext.Products.ToListAsync();
                if (Products != null && Products.Any())
                {
                    var results = mapper.Map<IEnumerable<DB.Product>, IEnumerable<Models.Product>>(Products);
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

        public async Task<(bool IsSuccess, Product? Product, string? ErrorMessage)> GetProductAsync(int id)
        {
            try
            {
                var Product = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (Product != null)
                {
                    var result = mapper.Map<DB.Product, Models.Product>(Product);
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
