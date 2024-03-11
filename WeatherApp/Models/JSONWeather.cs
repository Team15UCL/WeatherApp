using Newtonsoft.Json;

namespace WeatherApp.Models;

public class JSONWeather
{
    [JsonProperty("temp")]
    public double Temp;

    [JsonProperty("feels_like")]
    public double FeelsLike;

    [JsonProperty("temp_min")]
    public double TempMin;

    [JsonProperty("temp_max")]
    public double TempMax;

    [JsonProperty("pressure")]
    public int Pressure;

    [JsonProperty("humidity")]
    public int Humidity;
}
