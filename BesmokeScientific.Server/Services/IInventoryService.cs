namespace BesmokeScientific.Server.Services
{
    public interface IInventoryService
    {
        Task<bool> AdjustInventoryAsync(int inventoryStatusId, int inAmount, int outAmount);
    }
}
