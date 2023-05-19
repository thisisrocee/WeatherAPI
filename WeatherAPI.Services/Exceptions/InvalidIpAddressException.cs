namespace WeatherAPI.Services.Exceptions
{
    public class InvalidIpAddressException : Exception
    {
        public InvalidIpAddressException() : base("Provided ip address is not valid.") { }
    }
}
