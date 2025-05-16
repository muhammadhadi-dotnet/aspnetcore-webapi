using Microsoft.EntityFrameworkCore;
using WebApi.Data.Interfaces;
using WebApi.Migrations;
using WebApi.Model;
using WebApi.Model.DTO;

namespace WebApi.Data.Services
{
    public class ProductService :IProduct
    {
        private readonly MyDbContext _context;
        public ProductService(MyDbContext context)
        {
            _context = context;
        }
        public async Task Add(ProductDTO product)
        {
            if (product == null)
            {
                throw new Exception("Product is null");
            }

            Product model = new Product
            {
                Name = product.Name,
                Description = product.Description
            };
            _context.Products.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int Id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == Id);
            if (product == null) return ;
           _context.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            return  _context.Products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description
            }).ToList();
        }

        public async Task<ProductDTO> GetProductById(int Id)
        { 
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == Id);
            if (product == null) return null;
            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description
            }; 
        }

        public async Task Update(ProductDTO productDTO)
        {
            var product = await _context.Products.FirstOrDefaultAsync(i => i.Id == productDTO.Id);
            if (product == null) return ;
            product.Name = productDTO.Name;
            product.Description = productDTO.Description;
            await _context.SaveChangesAsync();
        }

        public async Task Save()
        {
           await _context.SaveChangesAsync();
        }
    }
}
