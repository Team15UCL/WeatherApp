
using Microsoft.AspNetCore.Mvc;

namespace WeatherAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddResponseCaching();

        builder.Services.AddControllers(options =>
        {
            options.CacheProfiles.Add("Default",
                new CacheProfile()
                {
                    Duration = 10,
                    VaryByQueryKeys = ["*"]
                });
        });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseSwagger();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwaggerUI();
        }

        app.UseResponseCaching();

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
