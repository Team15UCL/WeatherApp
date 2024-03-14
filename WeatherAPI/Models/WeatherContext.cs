using Microsoft.EntityFrameworkCore;

namespace WeatherAPI.Models;

public class WeatherContext(DbContextOptions options) : DbContext(options)
{
	public DbSet<WeatherData> WeatherDataSet { get; set; }
}
