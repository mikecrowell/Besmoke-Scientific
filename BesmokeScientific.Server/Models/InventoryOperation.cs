namespace BesmokeScientific.Server.Models
{
    public class InventoryOperation
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int ProductId { get; set; }
        public int InventoryAmountIn { get; set; }
        public int InventoryAmountOut { get; set; }

        public Product Product { get; set; }
    }
}
