using ConsigneService.Infrastructure.HttpService.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using ConsigneService.Infrastructure.HttpServices;
using ConsigneService.Application.Models.Contracts.Common;

namespace ConsigneService.Application.Services.ServiceIntegration
{
    public interface IIdentityService
    {
        Task<object> GenerateEmailToken(string email, string fullName, int tokenLifetime, string token);
        Task<object> ValidateEmailToken(string accesstoken, string fullName, string email, string systemToken);
        OperationResult<UserTokenValidationResponse> ValidateAllToken(string accessToken, string idToken, string apiResource, string clientResource, long serviceId);
        Task<object> ValidateClientToken(string accessToken, string apiResource, long serviceId);
    }
    public class IdentityService : IIdentityService
    {
        private readonly IOptions<ServicesUrl> _servicesUrl;
        private readonly IHttpService _httpService;
        public IdentityService(IOptions<ServicesUrl> servicesUrl, IHttpService httpService)
        {
            _servicesUrl = servicesUrl;
            _httpService = httpService;
        }
        public async Task<object> GenerateEmailToken(string email, string fullName, int tokenLifetime, string systemToken)
        {

            return await _httpService.SendAsync<string>(new ApiRequest()
            {
                ApiType = ApiType.POST,
                //Url = $"{BaseUrl.IdentityBaseUrl()}" +
                //$"{_servicesUrl.Value.IdentityService.GenerateEmailToken}?" +
                //$"email={email}&" +
                //$"fullName={fullName}&" +
                //$"tokenLifeTime={tokenLifetime}",
                AccessToken = systemToken
            });
        }
        public OperationResult<UserTokenValidationResponse> ValidateAllToken(string accessToken, string idToken, string apiResource, string clientResource, long serviceId)
        {
            return _httpService.SendAsync<OperationResult<UserTokenValidationResponse>>(new ApiRequest()
            {
                ApiType = ApiType.POST,
                //Url = $"{BaseUrl.IdentityBaseUrl()}" +
                //$"{_servicesUrl.Value.IdentityService.ValidateAll}",
                Data = new ValidateAllRequest() { AccessToken = accessToken, IdToken = idToken, ApiResource = apiResource, ClientResource = clientResource, ServiceId = serviceId },
                AccessToken = accessToken,
            }).Result;
        }
        public async Task<object> ValidateClientToken(string accessToken, string apiResource, long serviceId)
        {
            return await _httpService.SendAsync<OperationResult<UserTokenValidationResponse>>(new ApiRequest()
            {
                ApiType = ApiType.POST,
                //Url = $"{BaseUrl.IdentityBaseUrl()}" +
                //$"{_servicesUrl.Value.IdentityService.ValidateAll}?" +
                //$"accesstoken={accessToken}&" +
                //$"apiResource={apiResource}&" +
                //$"serviceId={serviceId}",
                AccessToken = accessToken,
            });
        }
        public async Task<object> ValidateEmailToken(string accesstoken, string fullName, string email, string systemToken)
        {

            return await _httpService.SendAsync<string>(new ApiRequest()
            {
                ApiType = ApiType.POST,
                //Url = $"{BaseUrl.IdentityBaseUrl()}" +
                //$"{_servicesUrl.Value.IdentityService.ValidateEmailToken}",
                Data = new ValidateTokenRequest { AccessToken = accesstoken, FullName = fullName, Email = email },
                AccessToken = systemToken
            });
        }
    }
}
