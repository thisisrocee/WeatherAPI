namespace WeatherAPI.Core.Models;

public class UserLocation : Entity
{
    public string IpAddress { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public string ActionTime { get; set; }

    public ICollection<WeatherInformation> WeatherInformation { get; set; }
}