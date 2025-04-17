using BesmokeScientific.Server.Data;
using Microsoft.EntityFrameworkCore;
using BesmokeScientific.Server.Models;

namespace BesmokeScientific.Server.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly InventoryDbContext _context;

        public InventoryService(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AdjustInventoryAsync(int productId, int inAmount, int outAmount)
        {
            var inventory = await _context.InventoryStatuses.FirstOrDefaultAsync(i => i.ProductId == productId);
            if (inventory == null) return false;

            int newCount = inventory.AvailableInventoryCount + inAmount - outAmount;
            if (newCount < 0) return false; // Prevent negative inventory

            // Update InventoryStatus
            inventory.AvailableInventoryCount = newCount;

            // Create InventoryOperation entry
            var operation = new InventoryOperation
            {
                ProductId = productId,
                InventoryAmountIn = inAmount,
                InventoryAmountOut = outAmount,
                DateTime = DateTime.UtcNow
            };

            _context.InventoryOperations.Add(operation);

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
