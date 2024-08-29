namespace WeatherApp.JsonModels
{
    public class ResponseJsonModel
    {
        public TemperaturesJsonModel? Temperatures { get; set; }
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
