using Newtonsoft.Json;
using WeatherApp.Enums;
using WeatherApp.JsonModels;

namespace WeatherApp.Services
{
    public class WeatherService : IWeatherService
    {
        private const string GenericErrorMessage = "An error occurred while fetching the weather data";

        public async Task<string> GetAsync(string city, TemperatureUnit unit, string apiKey)
        {
            using var httpClient = new HttpClient();

            WeatherJsonModel? weatherData = null;

            try
            {
                var response = await httpClient.GetAsync($"https://api.openweathermap.org/data/2.5/weather?q={city}&units={unit.ToString().ToLower()}&mode=json&appid={apiKey}");
                var content = await response.Content.ReadAsStringAsync();

                weatherData = JsonConvert.DeserializeObject<WeatherJsonModel>(content);
            }
            catch (Exception)
            {
                throw new Exception(GenericErrorMessage);
            }

            if (weatherData == null)
            {
                throw new Exception(GenericErrorMessage);
            }

            if (weatherData.Cod == 404)
            {
                throw new Exception(weatherData.Message);
            }

            if (string.IsNullOrEmpty(weatherData.Main.Temp))
            {
                throw new Exception(GenericErrorMessage);
            }

            return weatherData.Main.Temp;
        }
    }
}
