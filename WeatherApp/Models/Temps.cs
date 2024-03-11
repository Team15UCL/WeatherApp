using Newtonsoft.Json;

namespace WeatherApp.Models;

public class Temps
{
	[JsonProperty("temp")]
	public double Temp { get; set; }

	[JsonProperty("feels_like")]
	public double FeelsLike { get; set; }

	[JsonProperty("temp_min")]
	public double TempMin { get; set; }

	[JsonProperty("temp_max")]
	public double TempMax { get; set; }

	[JsonProperty("pressure")]
	public int Pressure { get; set; }

	[JsonProperty("humidity")]
	public int Humidity { get; set; }

	public int Rounded => (int)Math.Round(Temp);
	public int RoundedMin => (int)Math.Round(TempMin);
	public int RoundedMax => (int)Math.Round(TempMax);
}
