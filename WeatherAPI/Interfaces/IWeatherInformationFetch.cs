using WeatherAPI.Models;

namespace WeatherAPI.Interfaces;

public interface IWeatherInformationFetch
{
    string GetWeatherData(double latitude, double longitude);
}