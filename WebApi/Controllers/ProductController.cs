using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Data.Interfaces;
using WebApi.Model.DTO;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _product;
        private ResponseDto _responseDto;
        private readonly MyDbContext _context;
        public ProductController(IProduct product, MyDbContext context)
        {
            _product = product;
            _responseDto = new ResponseDto();
            _context = context;
        }

        [HttpGet]

        public async Task<ResponseDto> GetProducts()
        {
            try
            {
              IEnumerable<ProductDTO> obj = await _product.GetAll();
              _responseDto.Result = obj;
            }
            catch(Exception ex)
            {
                _responseDto.isSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return  _responseDto;
        }

        [HttpGet("id")]
        public async  Task<ResponseDto> GetById(int id){

            try
            {
                ProductDTO product = await _product.GetProductById(id);

                if (product == null)
                {
                    _responseDto.isSuccess = false;
                }
                _responseDto.Result = product;
            }
            catch(Exception ex)
            {
                _responseDto.isSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [HttpPost]
        public async Task<ResponseDto> Create(ProductDTO product)
        {
            try
            {
                if (product == null) _responseDto.isSuccess = false;
                await _product.Add(product);
                _responseDto.Result = product;

            }
            catch(Exception ex)
            {
                _responseDto.isSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [HttpPut("id")]
        public async Task<ResponseDto> update(ProductDTO productDTO)
        {
            try
            {
                await _product.Update(productDTO);
                _responseDto.Result = productDTO;
            }
            catch (Exception ex)
            {
                _responseDto.isSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }
        [HttpDelete("id")]
        public async Task<ResponseDto> Delete(int id)
        {
            try
            {
                await _product.Delete(id);
            }
            catch(Exception ex)
            {
                _responseDto.isSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }
    }
}
