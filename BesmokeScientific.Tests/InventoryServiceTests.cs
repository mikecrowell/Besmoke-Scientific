using BesmokeScientific.Server.Data;
using BesmokeScientific.Server.Models;
using BesmokeScientific.Server.Services;
using Microsoft.EntityFrameworkCore;

namespace BesmokeScientific.Tests
{
    public class InventoryServiceTests
    {
        private InventoryDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<InventoryDbContext>()
                .UseInMemoryDatabase(databaseName: "InventoryTestDb")
                .Options;
            return new InventoryDbContext(options);
        }

        [Fact]
        public async Task UpdateInventoryCount_ShouldUpdateAvailableInventory()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var service = new InventoryService(context);
            var status = new InventoryStatus { Id = 1, ProductId = 1, AvailableInventoryCount = 10 };
            context.InventoryStatuses.Add(status);
            await context.SaveChangesAsync();

            // Act
            await service.AdjustInventoryAsync(1, 5, 0); // Add 5

            // Assert
            var updated = await context.InventoryStatuses.FindAsync(1);
            Assert.Equal(15, updated.AvailableInventoryCount);
        }
    }
}
