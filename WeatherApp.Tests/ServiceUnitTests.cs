using WeatherApp.Services;
using Xunit;

namespace WeatherApp.Tests
{
    public class ServiceUnitTests
    {
        public const string apiKey = "acc91cb421e5e20cc8ee8c9b2edfd701";
        public readonly WeatherService weatherService;

        public ServiceUnitTests()
        {
            this.weatherService = new WeatherService();
        }

        [Theory]
        [InlineData("Sofia")]
        [InlineData("Kyustendil")]
        [InlineData("Varna")]
        public async Task TestGetWithCorrectData(string city)
        {   
            var result = await this.weatherService.GetAsync(city, Enums.TemperatureUnit.Metric, apiKey);

            Assert.NotEmpty(result);
        }

        [Theory]
        [InlineData("123123")]
        [InlineData("zxcvewfdvf")]
        public async Task TestGetWithIncorrectData(string city)
        {   
            Task result() => this.weatherService.GetAsync(city, Enums.TemperatureUnit.Metric, apiKey);

            var error = await Assert.ThrowsAsync<Exception>(() => result());

            Assert.Equal("city not found", error.Message);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task TestGetWithEmptyData(string city)
        {   
            Task result() => this.weatherService.GetAsync(city, Enums.TemperatureUnit.Metric, apiKey);

            var error = await Assert.ThrowsAsync<Exception>(() => result());

            Assert.Equal("An error occurred while fetching the weather data", error.Message);
        }

        [Theory]
        [InlineData("Sofia")]
        public async Task TestGetWithWrongApiKey(string city)
        {   
            Task result() => this.weatherService.GetAsync(city, Enums.TemperatureUnit.Metric, "incorrect-key");

            var error = await Assert.ThrowsAsync<Exception>(() => result());

            Assert.Equal("An error occurred while fetching the weather data", error.Message);
        }
    }
}
