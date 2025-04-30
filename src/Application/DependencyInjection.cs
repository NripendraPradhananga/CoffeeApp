using Application.Abstraction;
using Application.BrewCoffee.Services;
using Application.Common.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Register application services
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IApiCallCounterService, ApiCallCounterService>();

        // Register MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return services;
    }
}
