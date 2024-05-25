
namespace RideBackend.Infrastructure.HttpServices
{
    public interface IHttpService : IDisposable
    {
        Response responseModel { get; set; }
        Task<T> SendAsync<T>(ApiRequest apiRequest);
    }
}

