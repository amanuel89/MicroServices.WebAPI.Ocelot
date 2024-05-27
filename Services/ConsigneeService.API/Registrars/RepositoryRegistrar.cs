

using Google.Api;
using CommonService.Application.Services;

namespace CommonService.API.Registrars
{
    public class RepositoryRegistrar : IWebApplicationBuilderRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {    
            builder.Services.AddScoped(typeof(IRepositoryBase<Bank>), typeof(RepositoryBase<Bank>));
        }
    }
}
