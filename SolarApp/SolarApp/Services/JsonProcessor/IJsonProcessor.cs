using SolarApp.Models;

namespace SolarApp.Services.JsonProcessor;

public interface IJsonProcessor : IWeatherApiProcessor, ISunriseSunsetApiProcessor, ITimeZoneApiProcessor
{
}