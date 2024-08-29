using Newtonsoft.Json;
using WeatherApp.JsonModels;
using Xunit;

namespace WeatherApp.Tests
{
    public class ControllerUnitTests
    {
        public readonly HttpClient httpClient;

        public ControllerUnitTests()
        {
            this.httpClient = new HttpClient();
        }

        [Theory]
        [InlineData("Sofia")]
        [InlineData("Kyustendil")]
        [InlineData("Varna")]
        public async Task TestGetWeatherDataWithCorrectData(string city)
        {
            var response = await httpClient.GetAsync($"https://localhost:7201/api/weather/get/{city}");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseJsonModel>(content);

            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Null(result.ErrorMessage);
        }

        [Theory]
        [InlineData("123123")]
        [InlineData("zxcvewfdvf")]
        public async Task TestGetWeatherDataWithIncorrectData(string city)
        {
            var response = await httpClient.GetAsync($"https://localhost:7201/api/weather/get/{city}");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseJsonModel>(content);

            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Equal("city not found", result.ErrorMessage);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task TestGetWeatherDataWithEmptyData(string city)
        {
            var response = await httpClient.GetAsync($"https://localhost:7201/api/weather/get/{city}");
            
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
