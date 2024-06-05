using SolarApp.Models;

namespace SolarApp.Services.JsonProcessor;

public interface ISunriseSunsetApiProcessor
{
    SunriseSunset ProcessSunriseSunsetApiStringToSunriseSunset(City city, DateTime date, string data);
}