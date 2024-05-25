var builder = WebApplication.CreateBuilder(args);
builder.RegisterServices(typeof(Program));
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var app = builder.Build();
app.RegisterPipelineComponents(typeof(Program));
app.UseStaticFiles();
app.UseSession();
app.UseCors(MyAllowSpecificOrigins);
app.Run();
