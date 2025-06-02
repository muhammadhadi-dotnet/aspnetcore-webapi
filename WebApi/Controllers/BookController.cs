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


    public class BookController : ControllerBase
    {
        private readonly MyDbContext _context;
        private ResponseDto _response;
        public BookController(MyDbContext context) {
            _context = context;
            _response = new ResponseDto();
        }


        [HttpGet("GetBooks")]
        public async Task<ResponseDto> Index()
        {
            try
            {
                IEnumerable<Book> book = await _context.Books.ToListAsync();
                _response.Result = book;

            } catch (Exception ex) {
                _response.isSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost("addNewBook")]
        public async Task<ResponseDto> AddBook(Book book)
        {
            try
            {
                _context.Books.Add(book);
                await _context.SaveChangesAsync();
                _response.Message = "Book add successfuly";
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet("get-book-by-id/{id}")]
        public async Task<ResponseDto> GetBookById(int id)
        {
            try
            {
                Book book = await _context.Books.FirstAsync(i=>i.Id==id);
                if (book == null)
                {
                    _response.isSuccess=false;
                }
                _response.Result =  book
                ;

            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut("update-book/{id}")]
        public async Task<ResponseDto> UpdateBook(Book book)
        {
            try
            {
                var bookObj = await _context.Books.FirstAsync(i => i.Id == book.Id);
                if (bookObj == null)
                {
                    _response.isSuccess = false;
                }
                _context.Books.Update(bookObj);
                await _context.SaveChangesAsync();
                _response.Message = "Book update successfuly";

            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpDelete("delete-book/{id}")]
        public async Task<ResponseDto> Delete(int id)
        {
            try
            {
                Book book = await _context.Books.FirstAsync(i => i.Id == id);
                if (book == null)
                {
                    _response.isSuccess = false;
                }
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
                _response.Message = "Book delete successfuly";

            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }




    }
}
