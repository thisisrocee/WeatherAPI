using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WeatherAPI.Core.Models;
using WeatherAPI.Core.Services;
using WeatherAPI.Data;
using WeatherAPI.Services.Fetches;

namespace WeatherAPI.Services;

public class WeatherService : IWeatherService
{
    private readonly UserLocationFetch _ipDataService;
    private readonly WeatherInformationFetch _weatherDataService;
    private readonly IMemoryCache _cache;
    private readonly WeatherApiDbContext _context;

    public WeatherService(
        UserLocationFetch ipDataService,
        WeatherInformationFetch weatherDataService,
        IMemoryCache cache,
        WeatherApiDbContext context)
    {
        _ipDataService = ipDataService;
        _weatherDataService = weatherDataService;
        _cache = cache;
        _context = context;
    }

    public WeatherInformation GetWeatherInformation(string ipAddress)
    {
        var locationData = GetLocationData(ipAddress);
        if (locationData == null)
            return null;

        var weatherData = GetWeatherData(locationData.Latitude, locationData.Longitude);
        if (weatherData == null)
            return null;

        var weatherInformation = CreateWeatherInformation(weatherData);
        SaveWeatherInformation(weatherInformation);

        return weatherInformation;
    }

    private Location GetLocationData(string ipAddress)
    {
        var ipDataJson = _ipDataService.GetLocation(ipAddress);
        var ipDataResponse = JsonConvert.DeserializeObject<UserLocation>(ipDataJson.ToString());

        var latitude = ipDataResponse?.Latitude;
        var longitude = ipDataResponse?.Longitude;

        if (latitude != null && longitude != null)
        {
            var locationData = new Location
            {
                Latitude = latitude.Value,
                Longitude = longitude.Value
            };

            SaveUserLocation(ipAddress, locationData);

            return locationData;
        }

        return null;
    }

    private JObject GetWeatherData(double latitude, double longitude)
    {
        var weatherDataJson = _weatherDataService.GetWeatherData(latitude, longitude);
        return JsonConvert.DeserializeObject<JObject>(weatherDataJson);
    }

    private WeatherInformation CreateWeatherInformation(JObject weatherData)
    {
        return new WeatherInformation
        {
            Temperature = weatherData["main"]["temp"].Value<double>(),
            Description = weatherData["weather"][0]["description"].Value<string>(),
            FeelsLike = weatherData["main"]["feels_like"].Value<double>(),
            CityName = weatherData["name"].Value<string>(),
            Sunrise = weatherData["sys"]["sunrise"].Value<int>().ToString(),
            Sunset = weatherData["sys"]["sunset"].Value<int>().ToString(),
            Humidity = weatherData["main"]["humidity"].Value<int>(),
            UserId = GetUserLocationId()
        };
    }

    private void SaveWeatherInformation(WeatherInformation weatherInformation)
    {
        _context.WeatherInformations.Add(weatherInformation);
        _context.SaveChanges();
    }

    private void SaveUserLocation(string ipAddress, Location location)
    {
        var userLocation = new UserLocation
        {
            IpAddress = ipAddress,
            Longitude = location.Longitude,
            Latitude = location.Latitude,
            ActionTime = DateTime.Now.ToString(),
        };

        _context.UserLocations.Add(userLocation);
        _context.SaveChanges();

        _cache.Set("Latitude", location.Latitude);
        _cache.Set("Longitude", location.Longitude);
    }

    private int GetUserLocationId()
    {
        return _context.UserLocations.FirstOrDefault()?.Id ?? 0;
    }
}
