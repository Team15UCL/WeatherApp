using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using WeatherApp.Models;

namespace WeatherApp.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        Weather weather = await GetWeatherAsync();
        return View(weather);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private async Task<Weather> GetWeatherAsync()
    {
        HttpClient client = new()
        {
            BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/forecast")
        };

        var response = await client.GetStringAsync(client.BaseAddress + "?lat=56.1364&lon=9.1546&appid=747bcc2140e625521d195c8cb07c6ef0&units=metric");

        var test = JsonConvert.DeserializeObject<Weather>(response);

        return test;
    }
}
