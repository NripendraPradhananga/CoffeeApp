namespace Application.BrewCoffee;

public sealed record GetBrewCoffeeQueryResponse(BrewCoffeeStatus Status, string? Message, DateTime? Prepared);
