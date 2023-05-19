using RestSharp;
using WeatherAPI.Core.Services;

namespace WeatherAPI.Services.Fetches;

public class WeatherInformationFetch : IWeatherInformationFetch
{
    private readonly RestClient _restClient;
    private readonly string _apiKey = "4aa94cbb379421a00ee63b0d411ae1e8";

    public WeatherInformationFetch()
    {
        _restClient = new RestClient("https://api.openweathermap.org");
    }

    public string GetWeatherData(double latitude, double longitude)
    {
        var request = new RestRequest("data/2.5/weather");
        request.AddParameter("lat", latitude);
        request.AddParameter("lon", longitude);
        request.AddParameter("appid", _apiKey);
        request.AddParameter("units", "metric");

        var response = _restClient.Get(request);

        if (response.IsSuccessful)
        {
            return response.Content;
        }

        return null;
    }
}