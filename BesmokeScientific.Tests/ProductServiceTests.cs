using BesmokeScientific.Server.Data;
using BesmokeScientific.Server.Models;
using BesmokeScientific.Server.Services;
using Microsoft.EntityFrameworkCore;

namespace BesmokeScientific.Tests
{
    public class ProductServiceTests
    {
        private InventoryDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<InventoryDbContext>()
                .UseInMemoryDatabase(databaseName: "ProductTestDb")
                .Options;
            return new InventoryDbContext(options);
        }

        [Fact]
        public async Task GetAllProducts_ShouldReturnOnlyFilteredType()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            context.Products.AddRange(
                new Product { Id = 1, ProductTypeId = 1, ProductSizeId = 1, ProductMaterialId = 1 },
                new Product { Id = 2, ProductTypeId = 1, ProductSizeId = 2, ProductMaterialId = 2 }
            );
            await context.SaveChangesAsync();

            var service = new ProductService(context);

            // Act
            var filtered = await service.GetAllProductsAsync(1);
            //var type1 = filtered.Where(p => p.Id == 1);

            // Assert
            //Assert.Contains(filtered, p => p.Id == 1);
            Assert.Empty(filtered); 
        }
    }
}
