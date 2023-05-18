using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WeatherAPI.Data;
using WeatherAPI.Models;
using WeatherAPI.Services;

namespace WeatherAPI.Controllers;

[ApiController]
[Route("weather")]
public class WeatherController : ControllerBase
{
    private readonly IpDataService _ipDataService;
    private readonly WeatherDataService _weatherDataService;
    private readonly IMemoryCache _cache;
    private readonly WeatherApiDbContext _context;

    public WeatherController(IpDataService ipDataService, IMemoryCache cache, WeatherDataService weatherDataService, WeatherApiDbContext context)
    {
        _ipDataService = ipDataService;
        _cache = cache;
        _weatherDataService = weatherDataService;
        _context = context;
    }

    [HttpGet]
    public IActionResult GetWeather()
    {
        GetLocation();

        if (_cache.TryGetValue("Latitude", out double latitude) &&
            _cache.TryGetValue("Longitude", out double longitude))
        {
            var weatherDataJson = _weatherDataService.GetWeatherData(latitude, longitude);

            var weatherDataResponse = JsonConvert.DeserializeObject<JObject>(weatherDataJson);

            var temp = weatherDataResponse["main"]["temp"].Value<double>();
            var feelsLike = weatherDataResponse["main"]["feels_like"].Value<double>();
            var humidity = weatherDataResponse["main"]["humidity"].Value<int>();
            var sunrise = weatherDataResponse["sys"]["sunrise"].Value<int>();
            var sunset = weatherDataResponse["sys"]["sunset"].Value<int>();
            var name = weatherDataResponse["name"].Value<string>();
            var description = weatherDataResponse["weather"][0]["description"].Value<string>();

            var result = new WeatherInformation
            {
                Temperature = temp,
                Description = description,
                FeelsLike = feelsLike,
                CityName = name,
                Sunrise = sunrise.ToString(),
                Sunset = sunset.ToString(),
                Humidity = humidity,
                UserId = _context.UserLocations.First().Id
            };

            _context.WeatherInformations.Add(result);
            _context.SaveChanges();

            return Ok(result);
        }

        return BadRequest("Latitude and longitude not found in cache.");
    }

    private void GetLocation()
    {
        var ipAddress = GetUserIp();
        var ipDataJson = _ipDataService.GetLocation(ipAddress);

        var ipDataResponse = JsonConvert.DeserializeObject<UserLocation>(ipDataJson);

        var latitude = ipDataResponse.Latitude;
        var longitude = ipDataResponse.Longitude;

        var result = new UserLocation
        {
            IpAddress = ipAddress,
            Longitude = longitude,
            Latitude = latitude,
            ActionTime = DateTime.Now.ToString(),
        };

        _context.UserLocations.Add(result);
        _context.SaveChanges();

        _cache.Set("Latitude", latitude);
        _cache.Set("Longitude", longitude);
    }

    private string GetUserIp()
    {
        var ip = Response.HttpContext.Connection.RemoteIpAddress.ToString();

        if (ip == "::1")
        {
            ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString();
        }

        return ip;
    }
}