namespace BesmokeScientific.Server.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int ProductTypeId { get; set; }
        public int ProductSizeId { get; set; }
        public int ProductMaterialId { get; set; }

        public ProductType ProductType { get; set; }
        public ProductSize ProductSize { get; set; }
        public ProductMaterial ProductMaterial { get; set; }
    }
}
