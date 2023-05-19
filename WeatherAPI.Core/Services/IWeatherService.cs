using WeatherAPI.Core.Models;

namespace WeatherAPI.Core.Services;

public interface IWeatherService
{
    WeatherInformation GetWeatherInformation(string ipAddress);
}