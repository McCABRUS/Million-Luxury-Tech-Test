using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using RealEstate.Application.Services;
using RealEstate.Infrastructure.Persistence;
using RealEstate.Infrastructure.Repositories;
using RealEstate.Infrastructure.Settings;


var builder = WebApplication.CreateBuilder(args);
var frontendOrigin = builder.Configuration["FRONTEND_URL"] ?? "http://localhost:5173";
var imagesOptions = builder.Configuration.GetSection("ImagesOptions").Get<ImagesOptions>() ?? new ImagesOptions();
string configuredPath = imagesOptions.ImagesRootPath ?? string.Empty;
var projectDir = new DirectoryInfo(builder.Environment.ContentRootPath);
var repoRootDir = projectDir.Parent ?? projectDir;

if (string.IsNullOrWhiteSpace(configuredPath))
{
    configuredPath = Path.Combine(repoRootDir.FullName, "assets", "img", "propertyImgs");
}
else
{
    if (!Path.IsPathRooted(configuredPath))
    {
        configuredPath = Path.GetFullPath(Path.Combine(repoRootDir.FullName, configuredPath));
    }
}

if (!Directory.Exists(configuredPath))
{
    var fallback = Path.GetFullPath(Path.Combine(builder.Environment.ContentRootPath, configuredPath));
    if (Directory.Exists(fallback))
    {
        configuredPath = fallback;
    }
}

imagesOptions.ImagesRootPath = configuredPath;
imagesOptions.Validate();
builder.Services.AddSingleton(imagesOptions);

// Mongo settings and client
builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("MongoSettings"));
var mongoSettings = builder.Configuration.GetSection("MongoSettings").Get<MongoSettings>()
    ?? throw new InvalidOperationException("MongoSettings section not found in configuration");

var mongoClient = new MongoClient(mongoSettings.ConnectionString);
var database = mongoClient.GetDatabase(mongoSettings.DatabaseName);
builder.Services.AddSingleton(database);
builder.Services.AddSingleton<MongoContext>();

// Repositories and services
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

// Ensure the images root exists (warn if not)
if (!Directory.Exists(imagesOptions.ImagesRootPath))
{
    app.Logger.LogWarning("Images root path does not exist: {Path}", imagesOptions.ImagesRootPath);
}

// Serve static files from imagesRoot under imagesRequestPath (single registration)
var fileProvider = new PhysicalFileProvider(imagesOptions.ImagesRootPath);
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = fileProvider,
    RequestPath = new PathString(imagesOptions.ImagesRequestPath),
    ServeUnknownFileTypes = imagesOptions.ServeUnknownFileTypes,
    OnPrepareResponse = ctx =>
    {
        if (!string.IsNullOrEmpty(imagesOptions.CacheControlHeader))
            ctx.Context.Response.Headers["Cache-Control"] = imagesOptions.CacheControlHeader;
    }
});

app.UseRouting();
app.UseCors("FrontendCors");
app.UseAuthorization();
app.MapControllers();
app.Run();