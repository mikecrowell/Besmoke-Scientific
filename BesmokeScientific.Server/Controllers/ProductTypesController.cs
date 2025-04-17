using BesmokeScientific.Server.Data;
using BesmokeScientific.Server.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BesmokeScientific.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductTypesController : ControllerBase
    {
        private readonly InventoryDbContext _context;

        public ProductTypesController(InventoryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductTypeViewModel>>> GetProductTypes()
        {
            var types = await _context.ProductTypes
                .Select(t => new ProductTypeViewModel
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .ToListAsync();

            return Ok(types);
        }
    }
}
