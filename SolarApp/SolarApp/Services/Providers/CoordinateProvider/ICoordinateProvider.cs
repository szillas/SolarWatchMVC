namespace SolarApp.Services.Providers.CoordinateProvider;

public interface ICoordinateProvider
{
    Task<string> GetCityFromOpenWeatherMap(string city);
    Task<string> GetCityFromOpenWeatherMap(string city, string country);
    Task<string> GetCityFromOpenWeatherMap(string city, string country, string state);
}