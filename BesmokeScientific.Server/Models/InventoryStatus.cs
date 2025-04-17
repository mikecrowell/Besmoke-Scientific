namespace BesmokeScientific.Server.Models
{
    public class InventoryStatus
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int AvailableInventoryCount { get; set; }

        public Product Product { get; set; }
    }
}
