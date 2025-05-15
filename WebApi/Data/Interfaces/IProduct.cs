using WebApi.Model;
using WebApi.Model.DTO;

namespace WebApi.Data.Interfaces
{
    public interface IProduct
    {
        Task<IEnumerable<ProductDTO>> GetAll();
        Task<ProductDTO> GetProductById(int Id);

        Task Add(ProductDTO product);
        Task Update(ProductDTO product);
        Task Delete(int Id);
    }
}
