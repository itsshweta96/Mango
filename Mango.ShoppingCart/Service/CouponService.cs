using Mango.Services.Shared.Models;
using Mango.ShoppingCart.Models.Dto;
using Mango.ShoppingCart.Service.IService;
using Mango.ShoppingCart.Models.Dto;
using Newtonsoft.Json;

namespace Mango.ShoppingCart.Service
{
    public class CouponService : ICouponService
    {

        private readonly IHttpClientFactory _httpClient;
        public CouponService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CouponDto> GetCoupon(string couponCode)
        {
            var client = _httpClient.CreateClient("Coupon");
            var response = await client.GetAsync($"/api/coupons/GetByCode/{couponCode}");
            var apiContent = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
            if (responseDto != null && responseDto.IsSuccess)
            {
                return JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(responseDto.Result));
            }
            return new CouponDto();
        }
    }
}
