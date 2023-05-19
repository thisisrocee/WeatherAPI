using Microsoft.Extensions.Options;
using RestSharp;
using WeatherAPI.Core.Services;
using WeatherAPI.Services.Exceptions;

namespace WeatherAPI.Services.Fetches;

public class UserLocationFetch : IUserLocationFetch
{
    private readonly IRestClient _restClient;
    private readonly IOptions<IpDataConfig> _clientOptions;

    public UserLocationFetch(IRestClient restClient, IOptions<IpDataConfig> clientOptions)
    {
        _clientOptions = clientOptions;
        _restClient = restClient;
    }

    public async Task<string> GetLocation(string ipAddress)
    {
        if (string.IsNullOrEmpty(ipAddress))
            throw new InvalidIpAddressException();

        var request = new RestRequest($"{ipAddress}/weather");

        request.AddParameter("api-key", _clientOptions.Value.ApiKey);

        var response = await _restClient.ExecuteAsync(request);

        if (response.IsSuccessful)
        {
            return response.Content;
        }

        return null;
    }
}