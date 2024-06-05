namespace SolarApp.Services.Providers.TimeZoneProvider;

public interface ITimeZoneProvider
{
    Task<string> GetTimeZone(double latitude, double longitude);
}