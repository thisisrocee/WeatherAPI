﻿namespace WeatherAPI.Core.Models;

public class WeatherInformation : Entity
{
    public int UserId { get; set; }
    public UserLocation UserLocation { get; set; }

    public double Temperature { get; set; }
    public double FeelsLike { get; set; }
    public string CityName { get; set; }
    public string Description { get; set; }
    public string Sunrise { get; set; }
    public string Sunset { get; set; }
    public int Humidity { get; set; }
}