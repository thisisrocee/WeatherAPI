using System.Configuration;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WeatherAPI.Core.Services;
using WeatherAPI.Data;
using WeatherAPI.Data.Interfaces;
using WeatherAPI.Services;
using WeatherAPI.Services.Fetches;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();


builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddDbContext<WeatherApiDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("weather-api"));
});
builder.Services.AddTransient<IWeatherApiDbContext, WeatherApiDbContext>();
builder.Services.AddTransient<IWeatherService, WeatherService>();
builder.Services.AddSingleton<UserLocationFetch>();
builder.Services.AddSingleton<WeatherInformationFetch>();
builder.Services.Configure<IpDataConfig>
    (configuration.GetSection(nameof(IpDataConfig)));

builder.Services.AddMemoryCache();

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = 
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseForwardedHeaders();

app.UseAuthorization();

app.MapControllers();

app.Run();
