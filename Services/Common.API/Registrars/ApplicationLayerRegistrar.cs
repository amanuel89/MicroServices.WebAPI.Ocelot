using Google.Api;
using IdentityServer.Application.Services;
using IdentityServer.Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using CommonService.Application.Services;
using CommonService.Application.Services.Helper;
using CommonService.Infrastructure.HttpServices;
using System.Net.NetworkInformation;
using System.Reflection;
using Common.Application.Commands.CountryCommand;

namespace CommonService.API.Registrars
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

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateCountry>());
            builder.Services.AddAutoMapper(typeof(Program), typeof(CreateCountry));
        
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
