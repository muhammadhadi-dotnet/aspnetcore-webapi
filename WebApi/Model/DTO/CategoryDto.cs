using System.ComponentModel.DataAnnotations;

namespace WebApi.Model.DTO
{
    public class CategoryDto
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }
        [Required]
        public string Title { get; set; }
    }
}
