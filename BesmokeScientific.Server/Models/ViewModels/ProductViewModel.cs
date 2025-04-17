namespace BesmokeScientific.Server.Models.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Size { get; set; }
        public string Material { get; set; }
        public int AvailableInventoryCount { get; set; }
    }
}
