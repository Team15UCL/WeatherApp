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
        BaseAddress = new Uri("https://weatherapi20240318130019.azurewebsites.net/")
    };

    public WeatherController(ILogger<WeatherController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string? city = "ikast")
    {
        ViewData["city"] = city;

        IEnumerable<WeatherData> weather = await GetFiveDayFromOwnAPI(city);
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

    private async Task<IEnumerable<WeatherData>?> GetFiveDayFromOwnAPI(string city)
    {
        var response = await client.GetStringAsync(client.BaseAddress + $"5day?city={city}");

        var weatherResponse = JsonConvert.DeserializeObject<IEnumerable<WeatherData>>(response);

        return weatherResponse;
    }

    private async Task<IEnumerable<WeatherData>> Get24HourFromOwnAPI(string lat, string lon)
    {
        var response = await client.GetStringAsync(client.BaseAddress + $"24hour?lat={lat}&lon={lon}");

        var weatherResponse = JsonConvert.DeserializeObject<IEnumerable<WeatherData>>(response);

        return weatherResponse ?? [];
    }
}
