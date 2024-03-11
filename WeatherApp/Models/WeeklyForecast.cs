using Newtonsoft.Json;

namespace WeatherApp.Models;

public class WeeklyForecast
{
    [JsonProperty("dt")]
    public int Dt;

    public DayOfWeek Date => DateTime.UnixEpoch.AddSeconds(Dt).DayOfWeek;

    public string Time => DateTime.UnixEpoch.AddSeconds(Dt).ToShortTimeString();

    [JsonProperty("weather")]
    public List<Description> Description;

    [JsonProperty("main")]
    public Temps Temps;
}