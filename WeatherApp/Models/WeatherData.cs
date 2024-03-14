namespace WeatherApp.Models;

public class WeatherData
{
	public int Id { get; set; }

	public string Day { get; set; }

	public string Time { get; set; }

	public string City { get; set; }

	public int Temperature { get; set; }

	public int MaxTemp { get; set; }

	public int MinTemp { get; set; }

	public string WeatherCondition { get; set; }

	public string Description { get; set; }

	public string Icon { get; set; }
}
