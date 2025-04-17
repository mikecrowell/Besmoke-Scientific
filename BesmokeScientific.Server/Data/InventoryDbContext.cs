using BesmokeScientific.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace BesmokeScientific.Server.Data 
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<ProductMaterial> ProductMaterials { get; set; }
        public DbSet<InventoryOperation> InventoryOperations { get; set; }
        public DbSet<InventoryStatus> InventoryStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<ProductType>().ToTable("ProductType");
            modelBuilder.Entity<ProductSize>().ToTable("ProductSize");
            modelBuilder.Entity<ProductMaterial>().ToTable("ProductMaterial");
            modelBuilder.Entity<InventoryOperation>().ToTable("InventoryOperation");
            modelBuilder.Entity<InventoryStatus>().ToTable("InventoryStatus");
        }
    }
}
