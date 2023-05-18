using Microsoft.AspNetCore.Mvc;
using System.Net;
using WeatherAPI.Interfaces;

namespace WeatherAPI.Controllers;

[ApiController]
[Route("weather")]
public class WeatherController : ControllerBase
{
    private readonly IWeatherService _weatherService;

    public WeatherController(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    [HttpGet]
    public IActionResult GetWeather()
    {
        var ipAddress = GetUserIp();

        var result = _weatherService.GetWeatherInformation(ipAddress);

        if (result != null)
        {
            return Ok(result);
        }

        return BadRequest("Latitude and longitude not found in cache.");
    }

    private string GetUserIp()
    {
        var ip = Response.HttpContext.Connection.RemoteIpAddress.ToString();

        if (ip == "::1")
        {
            ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString();
        }

        return ip;
    }
}