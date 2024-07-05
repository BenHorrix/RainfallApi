using System.Reflection;
using Microsoft.OpenApi.Models;
using RainfallApi.DocumentFilters;
using RainfallApi.Services.Implementations.RainfallMeasurement;
using RainfallApi.Services.RainfallMeasurement;

var builder = WebApplication.CreateBuilder(args);

// Add dependency injection
builder.Services.AddSingleton<IRainfallMeasurementService, GovernmentRainfallMeasurementService>();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "1.0",
        Title = "Rainfall Api",
        Description = "An API which provides rainfall reading data",
        Contact = new OpenApiContact
        {
            Name = "Sorted",
            Url = new Uri("https://www.sorted.com"),
        }
    });
    options.AddServer(new OpenApiServer { Url = "http://localhost:3000", Description = "Rainfall Api"});
    options.DocumentFilter<RootTagsDocumentFilter>();
    options.MapType<int>(() => new OpenApiSchema { Type = "number" });
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
    options.DescribeAllParametersInCamelCase();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
