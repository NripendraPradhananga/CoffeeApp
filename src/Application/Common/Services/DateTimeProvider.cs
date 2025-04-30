using Application.Abstraction;

namespace Application.Common.Services;

public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.Now;
    public DateTime UtcNow => DateTime.UtcNow;
}
