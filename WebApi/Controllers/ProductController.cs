using Microsoft.AspNetCore.Mvc;
using WebApi.Data.Interfaces;
using WebApi.Model.DTO;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _product;
        public ProductController(IProduct product)
        {
            _product = product;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            var products = await _product.GetAll();

            return Ok(products);
        }

        [HttpGet("id")]
        public async  Task<ActionResult<ProductDTO>> GetProductById(int id){
          var product =await _product.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO product)
        {
            if (product == null)
            {
                return BadRequest("Product is null");
            }
            await _product.Add(product);
            return CreatedAtAction(nameof(GetProductById), new {id=product.Id},product);
        }

        [HttpPost("id")]
        public async Task<ActionResult> update(ProductDTO productDTO)
        {

            await _product.Update(productDTO);
            return CreatedAtAction(nameof(GetProductById), new { id = productDTO.Id }, productDTO);

        }
        [HttpDelete("id")]
        public async Task<ActionResult> Delete(int id)
        {
            await _product.Delete(id);
            return NoContent();
        }
    }
}
