using Microsoft.Extensions.Options;
using Moq;
using RestSharp;
using WeatherAPI.Core.Services;
using WeatherAPI.Services;
using WeatherAPI.Services.Exceptions;
using WeatherAPI.Services.Fetches;

namespace WeatherAPI.Tests.Testings
{
    [TestFixture]
    public class UserLocationFetchTests
    {
        private AutoMocker _mocker;
        private IUserLocationFetch _userLocationFetch;

        [SetUp]
        public void Setup()
        {
            _mocker = new AutoMocker();
            _userLocationFetch = new UserLocationFetch(
                _mocker.GetMock<IRestClient>().Object, 
                _mocker.Get<IOptions<IpDataConfig>>());
        }

        [Test]
        public void GetLocation_ValidIpAddress_ShouldReturnLocationJson()
        {
            var ipAddress = "127.0.0.1";
            var expectedContent = "location json";
            var responseMock = _mocker.GetMock<RestResponse>();
            var clientMock = _mocker.GetMock<RestClient>();

            responseMock.Setup(r => r.IsSuccessful).Returns(true);
            responseMock.Setup(r => r.Content).Returns(expectedContent);

            clientMock.Setup(c => c.Get(It.IsAny<RestRequest>()))
                .Returns(responseMock.Object);
            
            var result = _userLocationFetch.GetLocation(ipAddress);
            
            result.Should().Be(expectedContent);
        }

        // [Test]
        // public void GetLocation_InvalidIpAddress_ShouldThrowInvalidIpAddressException()
        // {
        //     // Arrange
        //     string ipAddress = null;
        //
        //     // Act
        //     var action = () => _userLocationFetch.GetLocation(ipAddress);
        //
        //     // Assert
        //     action.Should().Throw<InvalidIpAddressException>();
        // }
    }
}