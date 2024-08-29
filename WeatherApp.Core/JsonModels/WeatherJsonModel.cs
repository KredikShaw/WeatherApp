namespace WeatherApp.JsonModels
{
    public class WeatherJsonModel
    {
        public WeatherMainJsonModel Main { get; set; } = new WeatherMainJsonModel();
        public int Cod { get; set; }
        public string? Message { get; set; }
    }

    public class WeatherMainJsonModel
    {
        public string? Temp { get; set; }
    }
}
