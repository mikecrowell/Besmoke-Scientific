using BesmokeScientific.Server.Data;
using BesmokeScientific.Server.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BesmokeScientific.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductSizeController : ControllerBase
    {
        private readonly InventoryDbContext _context;

        public ProductSizeController(InventoryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductSizeViewModel>>> GetProductSizes()
        {
            var sizes = await _context.ProductSizes
                .Select(s => new ProductSizeViewModel { Id = s.Id, Name = s.Name })
                .ToListAsync();

            return Ok(sizes);
        }
    }
}
