using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json");
builder.Services.AddOcelot();

var app = builder.Build();

app.MapGet("/", () => "Hello World!"); // Define the home page response

// Configure other middleware or endpoints here if needed

app.UseOcelot().Wait();

app.Run();
