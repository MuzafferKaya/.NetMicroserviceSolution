using Mongo.Services.ShoppingCartAPI.Models.Dto;

namespace Mongo.Services.ShoppingCartAPI.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
