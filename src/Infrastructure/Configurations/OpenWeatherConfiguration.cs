namespace Infrastructure.Configurations;

public class OpenWeatherConfiguration
{
    public string ApiKey { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Unit { get; set; } = "metric";
}
