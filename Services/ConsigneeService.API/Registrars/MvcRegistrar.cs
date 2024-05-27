

using Asp.Versioning;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Hosting;
using CommonService.Infrastructure.Configurations;
using Quartz;
using System.Diagnostics.Tracing;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using App.Metrics.Formatters.Json;
using Prometheus;
using Common.Application.Models.Common;
namespace CommonService.API.Registrars
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
            builder.Services.Configure<KestrelServerOptions>(Options =>
            {
                Options.AllowSynchronousIO = true;
            });
            //builder.Services.AddMetrics();
            builder.Services.AddOpenTelemetry()
               .WithTracing(builder =>
               {
                   builder
                       .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("CommonService"))
                       .AddAspNetCoreInstrumentation()
                       .AddHttpClientInstrumentation()
                       .AddJaegerExporter();
               })
               .WithMetrics(builder =>
               {
                   builder
                       .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("CommonService"))
                       .AddAspNetCoreInstrumentation()
                       .AddHttpClientInstrumentation()
                        .AddRuntimeInstrumentation()
                       .AddPrometheusExporter();
               });
            var kestrelCounterListener = new KestrelEventListener();

        }
    }

    public class KestrelEventListener : EventListener
    {
        private readonly Dictionary<string, Gauge> _gauges = new Dictionary<string, Gauge>();

        protected override void OnEventSourceCreated(EventSource eventSource)
        {
            if (eventSource.Name == "Microsoft-AspNetCore-Server-Kestrel")
            {
                EnableEvents(eventSource, EventLevel.Verbose, EventKeywords.All);
            }
        }

        protected override void OnEventWritten(EventWrittenEventArgs eventData)
        {
            if (eventData.EventName == "EventCounters")
            {
                var payload = eventData.Payload[0] as IDictionary<string, object>;
                var name = payload["Name"] as string;
                var value = payload.ContainsKey("Mean") ? (double)payload["Mean"] : 0;

                // Adjust the metric name to match the required regex pattern
                var adjustedName = name.Replace("-", "_");

                if (!_gauges.ContainsKey(adjustedName))
                {
                    _gauges[adjustedName] = Metrics.CreateGauge(adjustedName, "Kestrel metric");
                }

                _gauges[adjustedName].Set(value);

            }
        }
    }
}
