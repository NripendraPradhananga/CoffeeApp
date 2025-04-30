using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstraction;

public interface IApplicationDbContext
{
    DbSet<ApiCallCounter> ApiCallCounters { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
