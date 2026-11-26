using Mango.Services.Shared.Models;
using Mango.ShoppingCart.Models.Dto;
using Mango.ShoppingCart.Service.IService;
using Newtonsoft.Json;

namespace Mango.ShoppingCart.Service
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClient;
        public ProductService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }   

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var client = _httpClient.CreateClient("Product");
            var response = await client.GetAsync($"/api/products");
            var apiContent = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
            if (responseDto != null && responseDto.IsSuccess)
            {
                return JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(Convert.ToString(responseDto.Result));
            }
            return new List<ProductDto>();
        }
    }
}
