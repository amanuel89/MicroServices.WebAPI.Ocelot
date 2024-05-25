

using Google.Api;
using RideBackend.Application.Services;

namespace RideBackend.API.Registrars
{
    public class RepositoryRegistrar : IWebApplicationBuilderRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {    
            builder.Services.AddScoped(typeof(IRepositoryBase<Address>), typeof(RepositoryBase<Address>));
            builder.Services.AddScoped(typeof(IRepositoryBase<Passenger>), typeof(RepositoryBase<Passenger>));
            builder.Services.AddScoped(typeof(IRepositoryBase<Driver>), typeof(RepositoryBase<Driver>));
            builder.Services.AddScoped(typeof(IRepositoryBase<Vehicle>), typeof(RepositoryBase<Vehicle>));
            builder.Services.AddScoped(typeof(IRepositoryBase<VehicleTypes>), typeof(RepositoryBase<VehicleTypes>));
            builder.Services.AddScoped(typeof(IRepositoryBase<VerificationCodes>), typeof(RepositoryBase<VerificationCodes>));
            builder.Services.AddScoped(typeof(IRepositoryBase<Bank>), typeof(RepositoryBase<Bank>));
            builder.Services.AddScoped(typeof(IRepositoryBase<Ride>), typeof(RepositoryBase<Ride>));
            builder.Services.AddScoped(typeof(IRepositoryBase<Tariff>), typeof(RepositoryBase<Tariff>));
            builder.Services.AddScoped(typeof(IRepositoryBase<RideSettings>), typeof(RepositoryBase<RideSettings>));
        }
    }
}
