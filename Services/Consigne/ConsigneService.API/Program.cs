using Serilog;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;
using OpenTelemetry.Exporter;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

builder.RegisterServices(typeof(Program));

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var app = builder.Build();

app.RegisterPipelineComponents(typeof(Program));
app.UseSession();
app.UseCors(MyAllowSpecificOrigins);
app.Run();
