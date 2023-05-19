using Microsoft.AspNetCore.Mvc;
using WeatherAPI.Data;

namespace WeatherAPI.Web.Controllers;

[ApiController]
[Route("cleanup-api")]
public class CleanupController : ControllerBase
{
    private readonly WeatherApiDbContext _context;

    public CleanupController(WeatherApiDbContext context)
    {
        _context = context;
    }

    [HttpPost("clear")]
    public IActionResult Clear()
    {
        _context.WeatherInformations
            .RemoveRange(_context.WeatherInformations);
        _context.UserLocations
            .RemoveRange(_context.UserLocations);
        _context.SaveChanges();

        return Ok();
    }
}