using Microsoft.AspNetCore.Mvc;
using WeatherApp.Enums;
using WeatherApp.JsonModels;
using WeatherApp.Services;

namespace WeatherApp.Controllers
{
    [Route("api/weather")]
    public class WeatherApiController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IWeatherService weatherService;

        public WeatherApiController(
            IConfiguration configuration,
            IWeatherService weatherService)
        {
            this.configuration = configuration;
            this.weatherService = weatherService;
        }

        [HttpGet]
        [Route("get/{city}")]
        public async Task<IActionResult> GetWeatherData(string city)
        {
            var celsius = string.Empty;
            var fahrenheit = string.Empty;
            var kelvin = string.Empty;

            var apiKey = configuration.GetValue<string>("APIKey");

            if (string.IsNullOrEmpty(apiKey))
            {
                var errorResponse = new ResponseJsonModel
                {
                    Success = false,
                    ErrorMessage = "API Key not found",
                };

                return this.Json(errorResponse);
            }

            try
            {
                celsius = await this.weatherService.GetAsync(city, TemperatureUnit.Metric, apiKey);
                fahrenheit = await this.weatherService.GetAsync(city, TemperatureUnit.Imperial, apiKey);
                kelvin = await this.weatherService.GetAsync(city, TemperatureUnit.Standard, apiKey);
            }
            catch (Exception e)
            {
                var errorResponse = new ResponseJsonModel
                {
                    Success = false,
                    ErrorMessage = e.Message,
                };

                return this.Json(errorResponse);
            }

            var temperatures = new TemperaturesJsonModel
            {
                TemperatureC = celsius,
                TemperatureF = fahrenheit,
                TemperatureK = kelvin,
            };

            var response = new ResponseJsonModel
            {
                Success = true,
                Temperatures = temperatures,
            };

            return this.Json(response);
        }
    }
}
