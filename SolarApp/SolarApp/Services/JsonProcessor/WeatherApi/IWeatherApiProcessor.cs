using SolarApp.Models;

namespace SolarApp.Services.JsonProcessor.WeatherApi;

public interface IWeatherApiProcessor
{
    Task<City> ProcessWeatherApiCityStringToCity(string data);
}