using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using Microsoft.OpenApi.Models;
using RealEstate.Application.Services;
using RealEstate.Infrastructure.Persistence;
using RealEstate.Infrastructure.Repositories;
using RealEstate.Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("MongoSettings"));
var mongoSettings = builder.Configuration.GetSection("MongoSettings").Get<MongoSettings>()
    ?? throw new InvalidOperationException("MongoSettings section not found in configuration");

// MongoDB client and database
var mongoClient = new MongoClient(mongoSettings.ConnectionString);
var database = mongoClient.GetDatabase(mongoSettings.DatabaseName);
builder.Services.AddSingleton(database);
builder.Services.AddSingleton<MongoContext>();

// Repositories and Services
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddScoped<PropertyService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "RealEstate API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RealEstate API v1"));
}

app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();