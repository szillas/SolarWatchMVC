namespace SolarApp.Services.Providers.TimeZoneProvider;

public class TimeApiProvider : ITimeZoneProvider
{
    private readonly ILogger<TimeApiProvider> _logger;

    public TimeApiProvider(ILogger<TimeApiProvider> logger)
    {
        _logger = logger;
    }
    
    public async Task<string> GetTimeZone(double latitude, double longitude)
    {
        var url = $"https://timeapi.io/api/TimeZone/coordinate?latitude={latitude}&longitude={longitude}";

        using var client = new HttpClient();

        _logger.LogInformation("Calling TimeApi API for TimeZone with url: {url}", url);

        var response = await client.GetAsync(url);
        return await response.Content.ReadAsStringAsync();
    }
}