using SolarApp.Models;

namespace SolarApp.Services.JsonProcessor;

public interface IWeatherApiProcessor
{
    Task<City> ProcessWeatherApiCityStringToCity(string data);
}