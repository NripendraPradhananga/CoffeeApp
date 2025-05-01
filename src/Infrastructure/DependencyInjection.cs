using Application.Abstraction;
using Infrastructure.Services;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Configurations;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Configure settings
        services.Configure<OpenWeatherConfiguration>(configuration.GetSection("OpenWeather"));
        

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase("CoffeeAppDb"));
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        services.AddHttpClient<IWeatherService, OpenWeatherWeatherService>(client =>
        {
            client.BaseAddress = new Uri(configuration["OpenWeather:BaseUrl"]);
        });
        return services;
    }
}
