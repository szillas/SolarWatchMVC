namespace SolarApp.Services.Providers.SunriseSunsetProvider;

public interface ISunriseSunsetProvider
{
    Task<string> GetSunriseSunset(double latitude, double longitude, string? date, string? timeZone);
}