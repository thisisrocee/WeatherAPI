using Microsoft.EntityFrameworkCore;
using WeatherAPI.Interfaces;
using WeatherAPI.Models;

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