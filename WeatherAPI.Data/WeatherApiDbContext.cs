using Microsoft.EntityFrameworkCore;
using WeatherAPI.Core.Models;
using WeatherAPI.Data.Interfaces;

namespace WeatherAPI.Data;

public class WeatherApiDbContext : DbContext, IWeatherApiDbContext
{
    public WeatherApiDbContext(DbContextOptions<WeatherApiDbContext> options) : base(options) { }

    public DbSet<WeatherInformation> WeatherInformations { get; set; }
    public DbSet<UserLocation> UserLocations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WeatherInformation>()
            .HasOne(w => w.UserLocation)
            .WithMany(u => u.WeatherInformation)
            .HasForeignKey(w => w.UserId);
    }
}