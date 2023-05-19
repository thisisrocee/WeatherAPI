using System.ComponentModel.DataAnnotations;

namespace WeatherAPI.Core.Models
{
    public abstract class Entity
    {
        [Key] public int Id { get; set; }
    }
}
