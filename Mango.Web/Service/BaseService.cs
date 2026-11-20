using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;
using System.Text;
using System.Text.Json;


namespace Mango.Web.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private ITokenService _tokenService;
        public BaseService(IHttpClientFactory httpClientFactory, ITokenService tokenService)
        {
            _httpClientFactory = httpClientFactory;
            _tokenService = tokenService;
        }
        public async Task<ResponseDto>? SendAsync(RequestDto requestDto, bool withBearerToken = true) // name shoul SendAsync can not be changed because it is an interface method
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("MangoAPI");
                HttpRequestMessage message = new(); //message sending to the API from web application.
                message.Headers.Add("Accept", "application/json"); // response in JSON format.

                //token 

                if (withBearerToken)
                {
                    var token = _tokenService.GetToken();
                    if (!string.IsNullOrEmpty(token))
                    {
                        message.Headers.Add("Authorization", $"Bearer {token}");
                    }
                }

                // message.RequestUri = new Uri(requestDto.Url);
                message.RequestUri = new Uri(requestDto.Url!);
                if (requestDto.Data != null) //need to serialize the data to JSON format for POST and PUT requests and send it in the request body
                {
                    message.Content = new StringContent(JsonSerializer.Serialize(requestDto.Data), Encoding.UTF8, "application/json");
                }

                message.Method = requestDto.ApiType switch
                {
                    SD.ApiType.POST => HttpMethod.Post,
                    SD.ApiType.PUT => HttpMethod.Put,
                    SD.ApiType.DELETE => HttpMethod.Delete,
                    _ => HttpMethod.Get
                };

                HttpResponseMessage? apiResponse = null;
                apiResponse = await client.SendAsync(message); //should be SendAsync, call the API and get the response from the API.

                var apiContent = await apiResponse.Content.ReadAsStringAsync();

                switch (apiResponse.StatusCode)
                {
                    case System.Net.HttpStatusCode.Unauthorized:
                        return new ResponseDto
                        {
                            IsSuccess = false,
                            ErrorMessage = "You are not authorized to access this resource."
                        };
                    case System.Net.HttpStatusCode.Forbidden:
                        return new ResponseDto
                        {
                            IsSuccess = false,
                            ErrorMessage = "You do not have permission to access this resource."
                        };
                }
                var apiResponseDto = JsonSerializer.Deserialize<ResponseDto>(apiContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return apiResponseDto!;
            }
            catch (Exception ex)
            {
                var dto = new ResponseDto
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message.ToString()
                };
                return dto;
            }
        }
    }
}
