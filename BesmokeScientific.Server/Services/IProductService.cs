using BesmokeScientific.Server.Models.ViewModels;

namespace BesmokeScientific.Server.Services
{
    public interface IProductService
    {
        Task<bool> UpdateProductAsync(int id, ProductViewModel model);
        Task<(bool Success, string Message, int? CreatedProductId)> CreateProductAsync(ProductViewModel model);
        Task<bool> DeleteProductAsync(int id);
        Task<bool> AdjustInventoryAsync(int productId, int adjustmentAmount);
        Task<List<ProductViewModel>> GetAllProductsAsync(int? productTypeId);
    }
}
