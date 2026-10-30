using NSwag.AspNetCore;
using ProductosApi.Repositories;
using ProductosApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IProductoRepository, ProductoRepositoryInMemory>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUi(settings =>
    {
        settings.Path = "/swagger";
        settings.DocumentTitle = "API de productos";
        settings.EnableTryItOut = true;
        settings.SwaggerRoutes.Clear();
        settings.SwaggerRoutes.Add(new SwaggerUiRoute("Productos API", "/openapi/v1.json"));
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
