namespace Application.Common.Extensions;

public static class DateTimeExtensions
{
    /// <summary>
    /// Checks if the given DateTime is the 1st of April.
    /// </summary>
    /// <param name="dateTime">The DateTime to check.</param>
    /// <returns>True if the date is the 1st of April, otherwise false.</returns>
    public static bool IsFirstOfApril(this DateTime dateTime)
    {
        return dateTime.Day == 1 && dateTime.Month == 4;
    }
}
