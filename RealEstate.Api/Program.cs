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
var frontendOrigin = builder.Configuration["FRONTEND_URL"] ?? "http://localhost:5173";

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

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendCors", policy =>
    {
        policy.WithOrigins(frontendOrigin)      
            .AllowAnyHeader()                 
            .AllowAnyMethod()                 
            .AllowCredentials()               
            .SetPreflightMaxAge(TimeSpan.FromMinutes(10)); 
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "RealEstate API", Version = "v1" });
});

var app = builder.Build();


app.UseRouting();
app.UseCors("FrontendCors");
app.UseAuthorization();
app.MapControllers();
app.Run();