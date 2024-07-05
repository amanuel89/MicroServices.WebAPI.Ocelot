using Serilog;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;
using OpenTelemetry.Exporter;
using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;
using Hangfire.Logging;
using RabbitMq.Shared.Messaging;
using RabbitMq.Shared.HealthCheck;
using OrderService.Messaging.Listener;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Services.AddLogging();

builder.Services.AddServiceHealthCheck();

builder.Services.Configure<RabbitMqConfiguration>(options => builder.Configuration.GetSection("RabbitMq").Bind(options));
builder.Services.AddHostedService<CountryDeletedListener>();
builder.Host.UseMetricsWebTracking().UseMetrics(options =>
{
    options.EndpointOptions = endpointsOptions =>
    {
        endpointsOptions.MetricsTextEndpointOutputFormatter = new MetricsPrometheusTextOutputFormatter();
        endpointsOptions.MetricsEndpointOutputFormatter = new MetricsPrometheusProtobufOutputFormatter();
        endpointsOptions.EnvironmentInfoEndpointEnabled = false;
    };
}).ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});
//builder.Host.UseSerilog();

builder.RegisterServices(typeof(Program));

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var app = builder.Build();

app.RegisterPipelineComponents(typeof(Program));
app.UseSession();
app.UseCors(MyAllowSpecificOrigins);

app.Run();