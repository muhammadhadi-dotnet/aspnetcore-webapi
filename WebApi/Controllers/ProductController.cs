using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Model;
using WebApi.Model.DTO;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly MyDbContext _context;
        public ProductController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();

            return Ok(products);
        }

        [HttpGet("id")]
        public async  Task<ActionResult<ProductDTO>> GetProductById(int id){
          var product =await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
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

            Product model = new Product
            {
                Name = product.Name,
                Description = product.Description
            };
             _context.Products.Add(model);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProductById), new {id=product.Id},product);
        }

        [HttpPost("id")]
        public async Task<ActionResult> update(ProductDTO productDTO,int id)
        {
            
            if (productDTO == null)
            {
                return NotFound();
            }
            Product model = new Product
            {
                Name = productDTO.Name,
                Description = productDTO.Description
            };

            _context.Update(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductById), new { id = productDTO.Id }, productDTO);

        }
        [HttpDelete("id")]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
