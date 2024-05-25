using Microsoft.AspNetCore.Mvc.Controllers;

namespace RideBackend.API.Filters;
public class AuthorizationHandler : IAuthorizationFilter
{
    private readonly IIdentityService _identityService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private static string _enviromentVariable = Environment.GetEnvironmentVariable("RideBackend");
    private static long _serviceId = string.IsNullOrEmpty(_enviromentVariable) ? 4 : Convert.ToInt64(_enviromentVariable);
    public AuthorizationHandler(IHttpContextAccessor httpContextAccessor, IIdentityService _identityService)
    {
        _httpContextAccessor = httpContextAccessor;
        this._identityService = _identityService;
    }

    public async void OnAuthorization(AuthorizationFilterContext context)
    {
      //  read requested language from header
        var language = context.HttpContext.Request.Headers["Accept-Language"].ToString();
        ////set session language
        //_httpContextAccessor?.HttpContext?.Session.SetString("language", language);
        //if (context != null && context?.ActionDescriptor is ControllerActionDescriptor descriptor)
        //{
        //    // get api resource 
        //    string apiClaim = String.Format("{0}-{1}", descriptor.ControllerName, descriptor.ActionName);

        //    //get header values
        //    var serviceKey = context.HttpContext.Request.Headers["Servicekey"].ToString();
        //    var accessToken = context.HttpContext.Request.Headers["accessToken"].ToString();
        //    var idToken = context.HttpContext.Request.Headers["IdToken"].ToString();
        //    //var clientClaim = context.HttpContext.Request.Headers["clientClaim"].ToString();

        //    // check if the request is from trusted service
        //    if (serviceKey != "86rIsmabiYR0OuW1B6NHovQsmWB8")
        //    {
        //        if (!Anonymous.Contains(apiClaim))
        //        {
        //            if (string.IsNullOrEmpty(accessToken))
        //            {
        //                context.Result = new UnauthorizedObjectResult(new { message = "Access token is empty." });
        //                return;
        //            }
        //            // validate request using identityService
        //            var isValidRequest = _identityService.ValidateAllToken(accessToken, idToken, apiClaim, apiClaim, _serviceId);


        //            if (isValidRequest.IsError)
        //                context.Result = new UnauthorizedObjectResult(isValidRequest);
        //            else
        //            {
        //                _httpContextAccessor?.HttpContext?.Session.SetString("client", isValidRequest.Payload.ClientId);
        //                _httpContextAccessor?.HttpContext?.Session.SetString("user", isValidRequest.Payload.UserId);
        //                _httpContextAccessor?.HttpContext?.Session.SetString("accessToken", accessToken);
        //            }

        //        }
        //    }
        //}
        //else
        //    context.Result = new UnauthorizedObjectResult(new { message = "context is  empty." });
    }
    public List<string> Anonymous = new List<string>
    {   
        "Payment-GetPaymentMethods",
        "Actions-RideBackendClaimSeeder"
    };
}