namespace Application.Abstraction;

public interface IApiCallCounterService
{
    Task<int> IncrementCounterAsync(CancellationToken cancellationToken);
}
