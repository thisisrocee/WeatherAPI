using RestSharp;
using WeatherAPI.Interfaces;

namespace WeatherAPI.Fetches;

public class UserLocationFetch : IUserLocationFetch
{
    private readonly RestClient _restClient;
    private readonly string _apiKey = "406073b4979ec15c1836f112c7e1b8ffbcdf30a590c8d449d0748b38";

    public UserLocationFetch()
    {
        _restClient = new RestClient("https://api.ipdata.co");
    }

    public string GetLocation(string ipAddress)
    {
        var request = new RestRequest($"{ipAddress}/weather");
        request.AddParameter("api-key", _apiKey);
        var response = _restClient.Get(request);

        if (response.IsSuccessful)
        {
            return response.Content;
        }

        return null;
    }
}