using BesmokeScientific.Server.Models.ViewModels;
using BesmokeScientific.Server.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BesmokeScientific.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpPost("adjust")]
        public async Task<IActionResult> AdjustInventory([FromBody] InventoryAdjustmentViewModel model)
        {
            var success = await _inventoryService.AdjustInventoryAsync(model.ProductId, model.InAmount, model.OutAmount);
            if (!success)
                return BadRequest("Invalid adjustment or inventory record not found.");

            return Ok(new { message = "Inventory updated." });
        }
    }
}
