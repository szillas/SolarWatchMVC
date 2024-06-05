using SolarApp.Models;

namespace SolarApp.Services.JsonProcessor;

public interface IJsonProcessor
{
    Task<City> ProcessWeatherApiCityStringToCity(string data);
    
    SunriseSunset ProcessSunriseSunsetApiStringToSunriseSunset(City city, DateTime date, string data);

    string? ProcessTimeApiResultStringForTimeZone(string data);
}