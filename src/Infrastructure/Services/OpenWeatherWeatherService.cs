using Application.Abstraction;
using Application.Abstraction.Dtos;
using Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Infrastructure.Services;

public class OpenWeatherWeatherService(HttpClient httpClient, IOptions<OpenWeatherConfiguration> option) : IWeatherService
{
    private readonly OpenWeatherConfiguration _openWeatherConfiguration = option.Value;

    public async Task<WeatherResponse> GetWeatherAsync(string cityName)
    {
        var city = string.IsNullOrWhiteSpace(cityName) ? _openWeatherConfiguration.City : cityName;
        
        var url = $"?q={city}&appid={_openWeatherConfiguration.ApiKey}&units={_openWeatherConfiguration.Unit}";
        
        var response = await httpClient.GetAsync(url);
        var json = await response.Content.ReadAsStringAsync();

        var weather = JsonSerializer.Deserialize<OpenWeatherResponse>(json);
        return new WeatherResponse(weather.Main.Temp);
    }
}