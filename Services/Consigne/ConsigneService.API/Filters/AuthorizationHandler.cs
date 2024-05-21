namespace ConsigneService.API.Filters;
public class AuthorizationHandler : IAuthorizationFilter
{
    private readonly IIdentityService _identityService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private static string _enviromentVariable = Environment.GetEnvironmentVariable("ConsigneService");
    private static long _serviceId = string.IsNullOrEmpty(_enviromentVariable) ? 2 : Convert.ToInt64(_enviromentVariable);
    public AuthorizationHandler(IHttpContextAccessor httpContextAccessor, IIdentityService _identityService)
    {
        _httpContextAccessor = httpContextAccessor;
        this._identityService = _identityService;
    }

    public async void OnAuthorization(AuthorizationFilterContext context)
    {
        //
    }
    public List<string> Anonymous = new List<string>
    {

    };
}