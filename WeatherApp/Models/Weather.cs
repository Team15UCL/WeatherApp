using Newtonsoft.Json;

namespace WeatherApp.Models;

public class Weather
{
    [JsonProperty("list")]
    public List<WeeklyForecast>? WeatherList { get; set; }
}