using BesmokeScientific.Server.Data;
using BesmokeScientific.Server.Models;
using BesmokeScientific.Server.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BesmokeScientific.Server.Services
{
    public class ProductService : IProductService
    {
        private readonly InventoryDbContext _context;

        public ProductService(InventoryDbContext context)
        {
            _context = context;
        }

        // Get all products, with optional filter by productTypeId
        public async Task<List<ProductViewModel>> GetAllProductsAsync(int? productTypeId)
        {
            var query = _context.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductSize)
                .Include(p => p.ProductMaterial)
                .AsQueryable();

            if (productTypeId.HasValue)
            {
                query = query.Where(p => p.ProductTypeId == productTypeId.Value);
            }

            var products = await query.ToListAsync();

            var inventoryStatuses = await _context.InventoryStatuses
                .ToDictionaryAsync(s => s.ProductId, s => s.AvailableInventoryCount);

            var result = products.Select(p => new ProductViewModel
            {
                Id = p.Id,
                Type = p.ProductType?.Name ?? "Unknown",
                Size = p.ProductSize?.Name ?? "Unknown",
                Material = p.ProductMaterial?.Name ?? "Unknown",
                AvailableInventoryCount = inventoryStatuses.TryGetValue(p.Id, out var count) ? count : 0
            }).ToList();

            return result;
        }

        // Update an existing product
        public async Task<bool> UpdateProductAsync(int id, ProductViewModel model)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return false;

            var type = await _context.ProductTypes.FirstOrDefaultAsync(t => t.Name == model.Type);
            var size = await _context.ProductSizes.FirstOrDefaultAsync(s => s.Name == model.Size);
            var material = await _context.ProductMaterials.FirstOrDefaultAsync(m => m.Name == model.Material);

            if (type == null || size == null || material == null)
                return false;

            product.ProductTypeId = type.Id;
            product.ProductSizeId = size.Id;
            product.ProductMaterialId = material.Id;

            await _context.SaveChangesAsync();
            return true;
        }

        // Create a new product
        public async Task<(bool Success, string Message, int? CreatedProductId)> CreateProductAsync(ProductViewModel model)
        {
            var type = await _context.ProductTypes.FirstOrDefaultAsync(t => t.Name == model.Type);
            var size = await _context.ProductSizes.FirstOrDefaultAsync(s => s.Name == model.Size);
            var material = await _context.ProductMaterials.FirstOrDefaultAsync(m => m.Name == model.Material);

            if (type == null || size == null || material == null)
                return (false, "Invalid type, size, or material.", null);

            // Check for duplicates
            bool duplicateExists = await _context.Products.AnyAsync(p =>
                p.ProductTypeId == type.Id &&
                p.ProductSizeId == size.Id &&
                p.ProductMaterialId == material.Id);

            if (duplicateExists)
                return (false, "Duplicate product exists.", null);

            var newProduct = new Product
            {
                ProductTypeId = type.Id,
                ProductSizeId = size.Id,
                ProductMaterialId = material.Id
            };

            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();

            // Initialize inventory status with 0
            _context.InventoryStatuses.Add(new InventoryStatus
            {
                ProductId = newProduct.Id,
                AvailableInventoryCount = 0
            });

            await _context.SaveChangesAsync();

            return (true, "Product created.", newProduct.Id);
        }

        // Delete a product
        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return false;

            var inventoryStatus = await _context.InventoryStatuses.FirstOrDefaultAsync(s => s.ProductId == id);
            if (inventoryStatus != null)
            {
                _context.InventoryStatuses.Remove(inventoryStatus);
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AdjustInventoryAsync(int productId, int adjustmentAmount)
        {
            var inventory = await _context.InventoryStatuses.FirstOrDefaultAsync(i => i.ProductId == productId);
            if (inventory == null)
                return false;

            inventory.AvailableInventoryCount += adjustmentAmount;

            if (inventory.AvailableInventoryCount < 0)
                return false; // Prevent negative stock

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
