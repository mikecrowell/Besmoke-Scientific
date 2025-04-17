using BesmokeScientific.Server.Models.ViewModels;
using BesmokeScientific.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace BesmokeScientific.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<List<ProductViewModel>>> GetAll([FromQuery] int? productTypeId)
        {
            try
            {
                var products = await _productService.GetAllProductsAsync(productTypeId);
                return Ok(products);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR in /api/products: " + ex.Message);
                Console.WriteLine(ex.StackTrace);
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }

        // PUT: api/products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductViewModel product)
        {
            var success = await _productService.UpdateProductAsync(id, product);
            if (!success)
                return NotFound();

            return NoContent();
        }

        // POST: api/products
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductViewModel product)
        {
            var result = await _productService.CreateProductAsync(product);
            if (!result.Success)
                return BadRequest(result.Message);

            return CreatedAtAction(nameof(GetAll), new { id = result.CreatedProductId }, product);
        }

        // DELETE: api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _productService.DeleteProductAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
