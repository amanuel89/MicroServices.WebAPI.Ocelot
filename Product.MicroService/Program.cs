using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Product.MicroService.Pocos;
using Products.MicroService.Interfaces;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(async (context, next) =>
{

    string ocelotGatewayIPAddress = "127.0.0.1";
    var ocelotGatewayIP = IPAddress.Parse(ocelotGatewayIPAddress);

    // Get the remote IP address of the incoming request
    var remoteIpAddress = context.Connection.RemoteIpAddress;

    // Compare the IP addresses
    if (!IPAddress.IsLoopback(remoteIpAddress) && remoteIpAddress.Equals(ocelotGatewayIP))
    {
        // Reject the request if it doesn't come from the Ocelot gateway
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        await context.Response.WriteAsync("Forbidden: Direct access not allowed.");
        return;
    }

    await next();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
