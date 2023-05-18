namespace WeatherAPI.Interfaces;

public interface IUserLocationFetch
{
    string GetLocation(string ipAddress);
}