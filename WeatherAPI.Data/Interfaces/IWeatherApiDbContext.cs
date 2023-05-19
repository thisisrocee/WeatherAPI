using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WeatherAPI.Core.Models;

namespace WeatherAPI.Data.Interfaces;

public interface IWeatherApiDbContext
{
    DbSet<WeatherInformation> WeatherInformations { get; set; }
    DbSet<UserLocation> UserLocations { get; set; }
    public int SaveChanges();
}