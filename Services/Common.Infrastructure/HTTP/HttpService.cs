using CommonService.Infrastructure.HttpService.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace CommonService.Infrastructure.HttpServices
{
    public class HttpService : IHttpService
    {
        public Response responseModel { get; set; } = new Response();
        private IHttpClientFactory httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor?.HttpContext?.Session;
        public HttpService(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClient) {
            _httpContextAccessor = httpContextAccessor;

            this.httpClient = httpClient; }
        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }

        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            try
            {
                var client = httpClient.CreateClient("GetNetEcommerceApiService");
                client.DefaultRequestHeaders.Clear();
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.Headers.Add("Servicekey", "86rIsmabiYR0OuW1B6NHovQsmWB8");
                message.Headers.Add("clientId", _session?.GetString("client"));
                message.RequestUri = new Uri(apiRequest.Url);
                message.Method = _getMethodType(apiRequest.ApiType);
                message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiRequest.AccessToken);
                var response= await client.SendAsync(message);

                if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(content);
                }
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(content);
                }
                var emptyResponse = Activator.CreateInstance<T>();
                return emptyResponse;
            }
            catch (Exception ex)
            {
                return (T) (object) new Response
                {
                    DisplayMessage = "Error",
                    ErrorMessages = new List<string> { Convert.ToString(ex.Message) },
                    IsSuccess = false
                };
            }
        }

        private static HttpMethod _getMethodType(ApiType apiType) =>
            apiType switch
            {
                ApiType.GET => HttpMethod.Get,
                ApiType.POST => HttpMethod.Post,
                ApiType.PUT => HttpMethod.Put,
                ApiType.DELETE => HttpMethod.Delete,
                _ => throw new ArgumentException(message: "Invalid enum value", paramName: nameof(apiType))
            };
    }
}
