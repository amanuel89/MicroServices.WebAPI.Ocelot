

using Google.Api;
using CommonService.Application.Services;
using Common.Application.Messaging;

namespace CommonService.API.Registrars
{
    public class RepositoryRegistrar : IWebApplicationBuilderRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {    
            builder.Services.AddScoped(typeof(IRepositoryBase<Country>), typeof(RepositoryBase<Country>));
            builder.Services.AddScoped(typeof(IRepositoryBase<Currency>), typeof(RepositoryBase<Currency>));
            builder.Services.AddScoped(typeof(IRepositoryBase<ExchangeRate>), typeof(RepositoryBase<ExchangeRate>));
            builder.Services.AddScoped(typeof(IRepositoryBase<SystemLookup>), typeof(RepositoryBase<SystemLookup>));
            builder.Services.AddScoped(typeof(CountryDeletedMessagePublisher ));

        }
    }
}
