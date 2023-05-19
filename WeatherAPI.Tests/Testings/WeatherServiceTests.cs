// using WeatherAPI.Core.Services;
// using WeatherAPI.Services.Fetches;
//
// namespace WeatherAPI.Tests.Testings
// {
//     public class WeatherServiceTests
//     {
//         private IUserLocationFetch _fetch;
//         private AutoMocker _mocker;
//
//         [SetUp]
//         public void Setup()
//         {
//             _fetch = new UserLocationFetch();
//             _mocker = new AutoMocker();
//         }
//
//         [Test]
//         public void GetLocation_ValidIpAddressProvided_ShouldReturnLocationJson()
//         {
//             var ipAddress = "127.0.0.1";
//             var expectedResult = "Location JSON";
//
//             var result = _fetch.GetLocation(ipAddress);
//
//             result.Should().Be(expectedResult);
//         }
//     }
// }
