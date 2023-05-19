namespace WeatherAPI.Core.Services;

public interface IUserLocationFetch
{
    Task<string> GetLocation(string ipAddress);
}