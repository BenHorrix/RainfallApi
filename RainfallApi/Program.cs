using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.OpenApi.Models;
using RainfallApi.DocumentFilters;
using RainfallApi.Services.Implementations.RainfallMeasurement;
using RainfallApi.Services.RainfallMeasurement;

var builder = WebApplication.CreateBuilder(args);

// Add dependency injection
builder.Services.AddSingleton<IRainfallMeasurementService, GovernmentRainfallMeasurementService>();

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
});

builder.Services.Configure<ApiBehaviorOptions>(apiBehaviorOptions => {
    apiBehaviorOptions.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddSingleton<FileExtensionContentTypeProvider>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "1.0",
        Title = "RainfallController Api",
        Description = "An API which provides rainfall reading data",
        Contact = new OpenApiContact
        {
            Name = "Sorted",
            Url = new Uri("https://www.sorted.com"),
        }
    });
    options.AddServer(new OpenApiServer { Url = "http://localhost:3000", Description = "RainfallController Api"});
    options.DocumentFilter<RootTagsDocumentFilter>();
    options.MapType<int>(() => new OpenApiSchema { Type = "number" });
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
    options.DescribeAllParametersInCamelCase();
});

var corsPolicyKey = "TechTestAllowAllOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicyKey,
        policy =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyMethod();
            policy.AllowAnyHeader();
        });
});

var app = builder.Build();

//app.UseHttpsRedirection();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(corsPolicyKey);

app.UseAuthorization();

app.MapControllers();

app.Run();
