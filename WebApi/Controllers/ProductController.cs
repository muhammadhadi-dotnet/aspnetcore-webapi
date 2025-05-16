using Microsoft.AspNetCore.Mvc;
using WebApi.Data.Interfaces;
using WebApi.Model;
using WebApi.Model.DTO;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _product;
        private ResponseDto _responseDto;
        public ProductController(IProduct product)
        {
            _product = product;
            _responseDto = new ResponseDto();
        }

        [HttpGet]

        public async Task<ResponseDto> GetProducts()
        {
            try
            {
              IEnumerable<ProductDTO> obj = await _product.GetAll();
              _responseDto.Result = obj;
              _responseDto.isSuccess = true;

            }
            catch(Exception ex)
            {
                _responseDto.isSuccess = false;
                _responseDto.Message = ex.ToString();
            }
            return  _responseDto;
        }

        [HttpGet("id")]
        public async  Task<ResponseDto> GetById(int id){

            try
            {
                ProductDTO product = await _product.GetProductById(id);

                //if (product == null)
                //{
                //    _responseDto.isSuccess = false;
                //    _responseDto.Message = "Product Not Found";
                //}
                _responseDto.Result = product;
                _responseDto.isSuccess = true;
            }
            catch(Exception ex)
            {
                _responseDto.isSuccess = false;
                _responseDto.Message = ex.ToString();
            }
            return _responseDto;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO product)
        {
            if (product == null)
            {
                return BadRequest("Product is null");
            }
            await _product.Add(product);
            return CreatedAtAction(nameof(GetById), new {id=product.Id},product);
        }

        [HttpPost("id")]
        public async Task<ActionResult> update(ProductDTO productDTO)
        {

            await _product.Update(productDTO);
            return CreatedAtAction(nameof(GetById), new { id = productDTO.Id }, productDTO);

        }
        [HttpDelete("id")]
        public async Task<ActionResult> Delete(int id)
        {
            await _product.Delete(id);
            return NoContent();
        }
    }
}
