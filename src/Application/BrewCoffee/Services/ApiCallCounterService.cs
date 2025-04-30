using Application.Abstraction;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.BrewCoffee.Services;

public class ApiCallCounterService(IApplicationDbContext applicationDbContext) : IApiCallCounterService
{
    public async Task<int> IncrementCounterAsync(CancellationToken cancellationToken)
    {
        var counter = await applicationDbContext.ApiCallCounters.FirstOrDefaultAsync(cancellationToken);

        if (counter is null)
        {
            counter = new ApiCallCounter { Count = 1 };
            applicationDbContext.ApiCallCounters.Add(counter);
        }
        else
        {
            counter.Count++;
            applicationDbContext.ApiCallCounters.Update(counter);
        }

        await applicationDbContext.SaveChangesAsync(cancellationToken);
        return counter.Count;
    }
}
