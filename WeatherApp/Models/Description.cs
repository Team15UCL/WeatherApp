using Newtonsoft.Json;

namespace WeatherApp.Models;

public class Description
{
	[JsonProperty("id")]
	public int Id { get; set; }

	[JsonProperty("main")]
	public string Main { get; set; }

	[JsonProperty("description")]
	public string Short { get; set; }

	[JsonProperty("icon")]
	public string Icon { get; set; }
}
