using AutoMapper;
using WebApi.Model;
using WebApi.Model.DTO;

namespace WebApi
{
    public class MappingConfig
    {

        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Category, CategoryDto>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
