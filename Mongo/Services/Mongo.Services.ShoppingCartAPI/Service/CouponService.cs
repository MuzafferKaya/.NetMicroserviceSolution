using Mongo.Services.ShoppingCartAPI.Models.Dto;
using Mongo.Services.ShoppingCartAPI.Service.IService;
using Newtonsoft.Json;

namespace Mongo.Services.ShoppingCartAPI.Service
{
    public class CouponService : ICouponService
    {
        private readonly IHttpClientFactory _factory;

        public CouponService(IHttpClientFactory factory)
        {
            this._factory = factory;
        }

        public async Task<CouponDto> GetCoupon(string couponCode)
        {
            var client = _factory.CreateClient("Coupon");
            var response = await client.GetAsync($"/api/coupon/GetByCode/{couponCode}");
            var apiContent = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
            if (resp.IsSuccess)
            {
                return JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(resp.Results));
            }
            return new CouponDto();
        }
    }
}
