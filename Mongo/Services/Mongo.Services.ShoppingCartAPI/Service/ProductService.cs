using Mongo.Services.ShoppingCartAPI.Models.Dto;
using Mongo.Services.ShoppingCartAPI.Service.IService;

namespace Mongo.Services.ShoppingCartAPI.Service
{
    public class ProductService : IProductService
    {
        public Task<IEnumerable<ProductDto>> GetProducts()
        {
            throw new NotImplementedException();
        }
    }
}
