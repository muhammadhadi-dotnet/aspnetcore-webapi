using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Model;
using WebApi.Model.DTO;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly MyDbContext _dbContext;
        private ResponseDto _responseDto;
        private IMapper _mapper;
        public CategoryController(MyDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _responseDto = new ResponseDto();
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ResponseDto> Get()
        {
            try
            {
                IEnumerable<Category> category = await _dbContext.Categories.ToListAsync();
                _responseDto.Result = _mapper.Map<IEnumerable<CategoryDto>>(category);
            }
            catch (Exception ex)
            {
                _responseDto.isSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }
        [HttpGet]
        [Route("CategoryById/{id}")]
        public async Task<ResponseDto> GetCategoryById(int id)
        {
            try
            {
                Category category =await _dbContext.Categories.FirstAsync(i => i.Id == id);
                _responseDto.Result = _mapper.Map<CategoryDto>(category);
            }
            catch (Exception ex)
            {
                _responseDto.isSuccess = false;
                _responseDto.Message = ex.Message;
            }

            return _responseDto;
        }
        [HttpPost]
        public async Task<ResponseDto> Create(CategoryDto categoryDto)
        {
            try
            {    Category category =_mapper.Map<Category>(categoryDto);
                _dbContext.Categories.Add(category);
                await _dbContext.SaveChangesAsync();
                _responseDto.Result = _mapper.Map<CategoryDto>(category);
            }
            catch(Exception ex)
            {
                _responseDto.isSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }
        [HttpPut]
        public async Task<ResponseDto> put(CategoryDto categoryDto)
        {
            try
            {    Category category=_mapper.Map<Category>(categoryDto);
                _dbContext.Categories.Update(category);
                await _dbContext.SaveChangesAsync();
                _responseDto.Result = _mapper.Map<CategoryDto>(category);
            }
            catch (Exception ex)
            {
                _responseDto.isSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }
        [HttpDelete]
        [Route("DeleteCategory/{id}")]
        public async Task<ResponseDto> Delete(int id)
        {
            try
            {
                Category category = await _dbContext.Categories.FirstAsync(d => d.Id == id);
                _dbContext.Remove(category);
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                _responseDto.isSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

    }
}
