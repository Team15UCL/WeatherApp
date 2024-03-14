using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using WeatherApp.Models;

namespace WeatherApp.Controllers;
public class WeatherController : Controller
{
	private readonly ILogger<WeatherController> _logger;

	HttpClient client = new()
	{
		BaseAddress = new Uri("https://localhost:7022/api/weather/")
	};

	public WeatherController(ILogger<WeatherController> logger)
	{
		_logger = logger;
	}

	public async Task<IActionResult> Index()
	{
		IEnumerable<WeatherData> weather = await GetFiveDayFromOwnAPI("56", "9");
		return View(weather);
	}

	public async Task<IActionResult> Current()
	{
		IEnumerable<WeatherData> weather = await Get24HourFromOwnAPI("56", "9");
		return View(weather);
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}

	private async Task<IEnumerable<WeatherData>> GetFiveDayFromOwnAPI(string lat, string lon)
	{
		var response = await client.GetStringAsync(client.BaseAddress + $"5day?lat={lat}&lon={lon}");

		var weatherResponse = JsonConvert.DeserializeObject<IEnumerable<WeatherData>>(response);

		return weatherResponse ?? [];
	}

	private async Task<IEnumerable<WeatherData>> Get24HourFromOwnAPI(string lat, string lon)
	{
		var response = await client.GetStringAsync(client.BaseAddress + $"24hour?lat={lat}&lon={lon}");

		var weatherResponse = JsonConvert.DeserializeObject<IEnumerable<WeatherData>>(response);

		return weatherResponse ?? [];
	}
}
