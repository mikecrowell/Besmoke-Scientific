using BesmokeScientific.Server.Data;
using BesmokeScientific.Server.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BesmokeScientific.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductMaterialController : ControllerBase
    {
        private readonly InventoryDbContext _context;

        public ProductMaterialController(InventoryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductMaterialViewModel>>> GetProductMaterials()
        {
            var materials = await _context.ProductMaterials
                .Select(m => new ProductMaterialViewModel { Id = m.Id, Name = m.Name })
                .ToListAsync();

            return Ok(materials);
        }
    }
}
