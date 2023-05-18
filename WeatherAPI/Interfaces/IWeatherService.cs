using WeatherAPI.Models;

namespace WeatherAPI.Interfaces;

public interface IWeatherService
{
    WeatherInformation GetWeatherInformation(string ipAddress);
}