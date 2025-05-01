using Application.Abstraction.Dtos;

namespace Application.Abstraction;

public interface IWeatherService
{
    Task<WeatherResponse> GetWeatherAsync(string city);
}
