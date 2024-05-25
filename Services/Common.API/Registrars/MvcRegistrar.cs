

using Asp.Versioning;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Hosting;
using RideBackend.Infrastructure.Configurations;
using Quartz;

namespace RideBackend.API.Registrars
{
    public class MvcRegistrar : IWebApplicationBuilderRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddMvc(setupAction: options =>
            {
                options.Filters.Add(typeof(AuthorizationHandler));
                options.EnableEndpointRouting = false;
            });
            builder.Services.AddControllers(config =>
            {
                config.Filters.Add(typeof(ExceptionHandler));
            })
                .AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            builder.Services.Configure<Settings>(builder.Configuration.GetSection("Settings"));
            builder.Services.Configure<ServicesUrl>(builder.Configuration.GetSection("ServicesUrl"));
            builder.Services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;// response header tells preferable 
                config.ApiVersionReader = new UrlSegmentApiVersionReader();// read version from url
            }).AddApiExplorer();

            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("http://localhost:3000")
                                      .WithOrigins("https://jtcbx162-3000.uks1.devtunnels.ms")
                                            .AllowAnyHeader()
                                            .AllowAnyMethod()
                                            .AllowCredentials(); // <-- Add this line
                                  });
            });
            builder.Services.BuildServiceProvider();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddHangfire(configuration => configuration
     .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
     .UseSimpleAssemblyNameTypeSerializer()
     .UseRecommendedSerializerSettings().UseSqlServerStorage(builder.Configuration.GetConnectionString("Default"), new SqlServerStorageOptions
     {
         // CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
         // SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
         // QueuePollInterval = TimeSpan.Zero,
         // UseRecommendedIsolationLevel = true,
         // DisableGlobalLocks = true
     }));
            //.UsePostgreSqlStorage(builder.Configuration.GetConnectionString("Default"), new PostgreSqlStorageOptions
            //{
            //    // CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
            //    // SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
            //    //QueuePollInterval = TimeSpan.Zero,
            //    // UseRecommendedIsolationLevel = true,
            //    // DisableGlobalLocks = true


            //}));
            builder.Services.AddSignalR();
            builder.Services.AddHangfireServer();
        
        }
    }
}
