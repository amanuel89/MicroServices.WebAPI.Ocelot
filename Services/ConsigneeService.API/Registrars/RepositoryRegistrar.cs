

using Google.Api;
using ConsigneeService.Application.Services;

namespace ConsigneeService.API.Registrars
{
    public class RepositoryRegistrar : IWebApplicationBuilderRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {    
            builder.Services.AddScoped(typeof(IRepositoryBase<Bank>), typeof(RepositoryBase<Bank>));
        }
    }
}
