using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WeatherAPI.Converters;
using WeatherAPI.Models;

namespace WeatherAPI.Controllers;
[Route("/")]
[ApiController]
public class WeatherController : ControllerBase
{
    HttpClient client = new()
    {
        BaseAddress = new Uri("https://api.openweathermap.org/")
    };

    [HttpGet("5day")]
    [ResponseCache(CacheProfileName = "Default")]
    public async Task<ActionResult<List<WeatherData>>> GetFiveDayWeatherData(string? city)
    {
        if (city == null)
        {
            return new ContentResult()
            {
                StatusCode = StatusCodes.Status418ImATeapot,
                Content = "Du skal da lige huske at skrive længegrad og breddegrad, tumpe!"
            };
        }

        var cityresponse = await client.GetStringAsync($"geo/1.0/direct?q={city}&appid=747bcc2140e625521d195c8cb07c6ef0&limit=1");

        List<GeoRoot>? geo = JsonConvert.DeserializeObject<List<GeoRoot>>(cityresponse);

        string lat = geo[0].Lat.ToString();
        string lon = geo[0].Lon.ToString();

        var response = await client.GetStringAsync($"data/2.5/forecast?lat={lat}&lon={lon}&appid=747bcc2140e625521d195c8cb07c6ef0&units=metric");

        var forecast = JsonConvert.DeserializeObject<Root>(response);

        List<WeatherData> weatherDataSet = new();

        foreach (var item in forecast.List)
        {
            WeatherData weatherData = new()
            {
                Day = TimeStampConverters.ConvertToDay(item.Dt),
                Time = TimeStampConverters.ConvertToTime(item.Dt),
                City = forecast.City.Name,
                Temperature = (int)Math.Round(item.Main.Temp),
                MaxTemp = (int)Math.Round(item.Main.TempMax),
                MinTemp = (int)Math.Round(item.Main.TempMin),
                WeatherCondition = item.Weather[0].Main,
                Description = item.Weather[0].Description,
                Icon = item.Weather[0].Icon,
            };

            weatherDataSet.Add(weatherData);
        }

        return weatherDataSet;
    }

    [HttpGet("24hour")]
    public async Task<ActionResult<List<WeatherData>>> GetCurrentDayWeatherData(string? lat, string? lon)
    {
        if (lat == null || lon == null)
        {
            return new ContentResult()
            {
                StatusCode = StatusCodes.Status418ImATeapot,
                Content = "Du skal da lige huske at skrive længegrad og breddegrad, tumpe!"
            };
        }

        var response = await client.GetStringAsync($"data/2.5/forecast?lat={lat}&lon={lon}&appid=747bcc2140e625521d195c8cb07c6ef0&units=metric");

        var forecast = JsonConvert.DeserializeObject<Root>(response);

        List<WeatherData> weatherDataSet = new();

        int TwentyFourHourLimit = DateTime.Now.AddHours(24).ConvertToUnix();

        foreach (var item in forecast.List.Where(x => x.Dt < TwentyFourHourLimit))
        {
            WeatherData weatherData = new()
            {
                Day = TimeStampConverters.ConvertToDay(item.Dt),
                Time = TimeStampConverters.ConvertToTime(item.Dt),
                City = forecast.City.Name,
                Temperature = (int)Math.Round(item.Main.Temp),
                MaxTemp = (int)Math.Round(item.Main.TempMax),
                MinTemp = (int)Math.Round(item.Main.TempMin),
                WeatherCondition = item.Weather[0].Main,
                Description = item.Weather[0].Description,
                Icon = item.Weather[0].Icon,
            };

            weatherDataSet.Add(weatherData);
        }

        return weatherDataSet;
    }
}


