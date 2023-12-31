using AutoMapper;
using ECommerce.API.Products.DB;
using ECommerce.API.Products.Profile;
using ECommerce.API.Products.Provider;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task GetProductsReturnsAllproducts()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>().UseInMemoryDatabase(nameof(GetProductsReturnsAllproducts)).Options;

            var dbcontext = new ProductsDbContext(options);

            CreateProducts(dbcontext);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg=>cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

            var productsProvider = new ProductsProvider(dbcontext, null, mapper);

            var product = await productsProvider.GetProductsAsync();

            Assert.True(product.IsSuccess);
            Assert.True(product.Products.Any());
            Assert.Null(product.ErrorMessage);
        }

        [Fact]
        public async Task GetProductsReturnsProductUsingValidId()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>().UseInMemoryDatabase(nameof(GetProductsReturnsProductUsingValidId)).Options;

            var dbcontext = new ProductsDbContext(options);

            CreateProducts(dbcontext);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

            var productsProvider = new ProductsProvider(dbcontext, null, mapper);

            var product = await productsProvider.GetProductAsync(1);

            Assert.True(product.IsSuccess);
            Assert.NotNull(product.Product);
            Assert.True(product.Product.Id == 1);
            Assert.Null(product.ErrorMessage);
        }

        [Fact]
        public async Task GetProductsReturnsProductUsingInvalidId()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>().UseInMemoryDatabase(nameof(GetProductsReturnsProductUsingInvalidId)).Options;

            var dbcontext = new ProductsDbContext(options);

            CreateProducts(dbcontext);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

            var productsProvider = new ProductsProvider(dbcontext, null, mapper);

            var product = await productsProvider.GetProductAsync(-1);

            Assert.False(product.IsSuccess);
            Assert.Null(product.Product);            
            Assert.NotNull(product.ErrorMessage);
        }

        private void CreateProducts(ProductsDbContext dbContext)
        {
            for (int i = 1; i <= 10; i++)
            {
                dbContext.Products.Add(new Product()
                {
                    Id = i,
                    Name = Guid.NewGuid().ToString(),
                    Inventory = i + 10,
                    Price = (decimal)(i *3.14)
                });
            }

            dbContext.SaveChanges();
        }
    }
}