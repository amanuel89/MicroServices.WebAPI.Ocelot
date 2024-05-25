using Google.Api;
using IdentityServer.Application.Services;
using IdentityServer.Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using RideBackend.Application.Services;
using RideBackend.Application.Services.Helper;
using RideBackend.Infrastructure.HttpServices;
using System.Net.NetworkInformation;
using System.Reflection;

namespace RideBackend.API.Registrars
{
    public class ApplicationLayerRegistrar : IWebApplicationBuilderRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
     
            builder.Services.AddSession(options =>
            {
                options.IOTimeout = TimeSpan.FromMinutes(10);
            });
            builder.WebHost.ConfigureKestrel(k =>
            {
                k.Limits.MaxRequestHeadersTotalSize = 64 * 1024;
                k.Limits.MaxRequestBufferSize = 64 * 1024;
            });

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateAddress>());
            builder.Services.AddAutoMapper(typeof(Program), typeof(CreateAddress));
        
            // application layer DI  
            builder.Services.AddScoped<IHttpService, HttpService>();
            builder.Services.AddHttpClient<IIdentityService, IdentityService>();
            builder.Services.AddScoped<IIdentityService, IdentityService>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IEmailSender, EmailSenderService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<RedisConnectionContext>();
            builder.Services.AddScoped<IRedisHelper, RedisHelper>();
            builder.Services.AddScoped<ImageUploader>();
        }
    }
}
