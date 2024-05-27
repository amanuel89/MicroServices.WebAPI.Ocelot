using ConsigneeService.Application.Models;
using ConsigneeService.Infrastructure.HttpService.Models;
using Microsoft.Extensions.Options;
using ConsigneeService.Infrastructure.Configurations;
using ConsigneeService.Infrastructure.HttpServices;
using static ConsigneeService.Application.Services.ServiceIntegration.PaymentService;

namespace ConsigneeService.Application.Services.ServiceIntegration
{
    public interface IPaymentService
    {
        Task<T> Authenticate<T>();
        Task<List<TeleBirrPaymentOptionsDto>> GetPaymentOptions(string accessToken);
        Task<PaymentConfirmationResponseDto> ConfirmTransaction(PaymentApprovalRequestModel request, string accessToken);
        Task<T> AuthorizePayment<T>(PaymentRequestModel request, string accessToken);
    }
    public class PaymentService : IPaymentService
    {
        private readonly IOptions<ServicesUrl> _servicesUrl;
        private readonly IHttpService _httpService;
        public PaymentService(IOptions<ServicesUrl> servicesUrl, IHttpService httpService)
        {
            _servicesUrl = servicesUrl;
            _httpService = httpService;
        }


        public async Task<T> Authenticate<T>()
        {
            string password = "$2a$10$um.537vTrMuSalYEVoLUbOgCkuvGBmoViI08GBPGXbP8rYUmOtaK6";

            var requestData = new { Password = password };

            return await _httpService.SendAsync<T>(new ApiRequest()
            {
                ApiType = ApiType.POST,
                Url = $"{BaseUrl.TeleBirr()}{_servicesUrl.Value.PaymentService.Authentication}",
                Data = requestData
            });
        }

        public async Task<PaymentConfirmationResponseDto> ConfirmTransaction(PaymentApprovalRequestModel request, string accessToken)
        {
          
            return await _httpService.SendAsync<PaymentConfirmationResponseDto>(new ApiRequest()
            {
                ApiType = ApiType.POST,
                Url = $"{BaseUrl.TeleBirr()}{_servicesUrl.Value.PaymentService.Transaction}",
                AccessToken = accessToken,
                Data = request
            });
        }

        public async Task<T> AuthorizePayment<T>(PaymentRequestModel request, string accessToken)
        {

            return await _httpService.SendAsync<T>(new ApiRequest()
            {
                ApiType = ApiType.POST,
                Url = $"{BaseUrl.TeleBirr()}{_servicesUrl.Value.PaymentService.AuthorizePayment}",
                AccessToken = accessToken,
                Data = request
            });
        }

        public async Task<List<TeleBirrPaymentOptionsDto>> GetPaymentOptions(string accessToken)
        {
            return await _httpService.SendAsync<List<TeleBirrPaymentOptionsDto>>(new ApiRequest()
            {
                ApiType = ApiType.GET,
                Url = $"{BaseUrl.TeleBirr()}{_servicesUrl.Value.PaymentService.GetPaymentOptions}",
                AccessToken = accessToken
            });
        }
    }

}
