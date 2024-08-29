using WeatherApp.Enums;

namespace WeatherApp.Services
{
    public interface IWeatherService
    {
        public Task<string> GetAsync(string city, TemperatureUnit unit, string apiKey);
    }
}
