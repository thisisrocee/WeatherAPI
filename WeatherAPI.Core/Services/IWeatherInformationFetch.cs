namespace WeatherAPI.Core.Services;

public interface IWeatherInformationFetch
{
    string GetWeatherData(double latitude, double longitude);
}