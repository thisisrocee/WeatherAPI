using System.ComponentModel.DataAnnotations;

namespace WeatherAPI.Models;

public class UserLocation
{
    [Key] public int Id { get; set; }

    public string IpAddress { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public string ActionTime { get; set; }

    public ICollection<WeatherInformation> WeatherInformation { get; set; }
}