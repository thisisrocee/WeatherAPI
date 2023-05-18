using Microsoft.EntityFrameworkCore;
using WeatherAPI.Models;

namespace WeatherAPI.Interfaces;

public interface IWeatherApiDbContext
{
    DbSet<WeatherInformation> WeatherInformations { get; set; }
    DbSet<UserLocation> UserLocations { get; set; }
}