using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;
using ConsigneService.Application.Models.Contracts.Common;
using System.Text.Json.Serialization;

using ConsigneService.Application.Models.Contracts.Common;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using App.Metrics.Formatters.Json;
using System.Diagnostics.Tracing;
using Prometheus;

namespace ConsigneService.API.Registrars
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
            builder.Services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;// response header tells preferable 
                config.ApiVersionReader = new UrlSegmentApiVersionReader();// read version from url
            });
            builder.Services.AddVersionedApiExplorer(config =>
            {
                config.GroupNameFormat = "'v'VVV";
                config.SubstituteApiVersionInUrl = true;

            });

            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("localhost:3000")
                                                          .AllowAnyHeader()
                                                          .AllowAnyMethod()
                                                          .AllowAnyOrigin();
                                  });
            });
            builder.Services.BuildServiceProvider();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.Configure<KestrelServerOptions>(Options =>
            {
                Options.AllowSynchronousIO = true;
            });
            //builder.Services.AddMetrics();
            builder.Services.AddOpenTelemetry()
               .WithTracing(builder =>
               {
                   builder
                       .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("ConsigneService"))
                       .AddAspNetCoreInstrumentation()
                       .AddHttpClientInstrumentation()
                       .AddConsoleExporter()
                       .AddJaegerExporter();
               })
               .WithMetrics(builder =>
               {
                   builder
                       .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("ConsigneService"))
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
                var value = (double)payload["Mean"];

                if (!_gauges.ContainsKey(name))
                {
                    _gauges[name] = Metrics.CreateGauge(name, "Kestrel metric");
                }

                _gauges[name].Set(value);
            }
        } 
    }
}
