using AutoMapper;
using Mongo.Services.ProductAPI.Models;
using Mongo.Services.ProductAPI.Models.Dto;

namespace Mongo.Services.ProductAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, Product>().ReverseMap();                
            });
            return mappingConfig;
        }
    }
}
