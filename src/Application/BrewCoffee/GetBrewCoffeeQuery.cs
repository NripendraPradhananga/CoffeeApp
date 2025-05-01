using Application.Abstraction;
using Application.Common.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.BrewCoffee;

public sealed record GetBrewCoffeeQuery : IRequest<GetBrewCoffeeQueryResponse>;

public class GetBrewCoffeeQueryHandler(
    ILogger<GetBrewCoffeeQueryHandler> logger,
    IApiCallCounterService apiCallCounterService,
    IDateTimeProvider dateTimeProvider,
    IWeatherService weatherService) : IRequestHandler<GetBrewCoffeeQuery, GetBrewCoffeeQueryResponse>
{
    private const int ServiceUnavailableThreshold = 5;

    private const string DefaultMessage = "Your piping hot coffee is ready";
    private const string IcedCoffeeMessage = "Your refreshing iced coffee is ready";

    private const string DefaultCity = "Hamilton,NZ";

    public async Task<GetBrewCoffeeQueryResponse> Handle(GetBrewCoffeeQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Brewing coffee...");

        // Check if the current date is April 1st
        if (IsAprilFoolsDay())
        {
            return new GetBrewCoffeeQueryResponse(BrewCoffeeStatus.Teapot, null, null);
        }

        var count = await apiCallCounterService.IncrementCounterAsync(cancellationToken);

        // Check if the counter is divisible by threshold
        if (IsServiceUnavailable(count))
        {
            return new GetBrewCoffeeQueryResponse(BrewCoffeeStatus.ServiceUnavailable, null, null);
        }

        var message = await GetMessageBasedOnWeather();

        return new GetBrewCoffeeQueryResponse(BrewCoffeeStatus.Brewed, message, DateTime.Now);
    }

    private async Task<string> GetMessageBasedOnWeather()
    {
        var message = DefaultMessage;
        try
        {
            var weatherResponse = await weatherService.GetWeatherAsync(DefaultCity);
            if (weatherResponse.Temp > 30)
            {
                message = IcedCoffeeMessage;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to get weather data");
        }
        return message;
    }

    private bool IsAprilFoolsDay()
    {
        return dateTimeProvider.Now.IsFirstOfApril();
    }

    private bool IsServiceUnavailable(int count)
    {
        return count % ServiceUnavailableThreshold == 0;
    }
}
