using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using SolarApp.Models;
using SolarApp.Services.Extensions;
using SolarApp.Services.JsonProcessor;
using SolarApp.Services.Providers.CoordinateProvider;
using SolarApp.Services.Providers.SunriseSunsetProvider;
using SolarApp.Services.Providers.TimeZoneProvider;
using SolarApp.Services.Repository;

namespace SolarApp.Controllers;

public class SunriseSunsetController : Controller
{
    private readonly ILogger<SunriseSunsetController> _logger;
    private readonly ICoordinateProvider _coordinateDataProvider;
    private readonly ISunriseSunsetProvider _sunriseSunsetProvider;
    private readonly IJsonProcessor _jsonProcessor;
    private readonly ICityRepository _cityRepository;
    private readonly ISunriseSunsetRepository _sunriseSunsetRepository;
    private readonly ITimeZoneProvider _timeZoneProvider;

    public SunriseSunsetController(ILogger<SunriseSunsetController> logger, ICoordinateProvider coordinateDataProvider, 
        ISunriseSunsetProvider sunriseSunsetProvider, IJsonProcessor jsonProcessor, ICityRepository cityRepository, 
        ISunriseSunsetRepository sunriseSunsetRepository, ITimeZoneProvider timeZoneProvider)
    {
        _logger = logger;
        _coordinateDataProvider = coordinateDataProvider;
        _sunriseSunsetProvider = sunriseSunsetProvider;
        _jsonProcessor = jsonProcessor;
        _cityRepository = cityRepository;
        _sunriseSunsetRepository = sunriseSunsetRepository;
        _timeZoneProvider = timeZoneProvider;
    }
    
    public IActionResult Index(SunriseSunset model)
    {
        return View(model);
    }

    public async Task<IActionResult> GetSunriseSunset(string name, string date)
    {
        try
        {
            var city = await GetCityFromDbOrApi(name);
            var dateTime = date.ParseDateOrDefaultToToday();
            var sunriseSunset = await GetSunFromDbOrApi(city, dateTime, date);
            
            return View("Index", sunriseSunset);
            
        }
        catch (JsonException e)
        {
            _logger.LogError(e, "Error processing API call.");
            return BadRequest(e.Message);
        }
        catch (FormatException e)
        {
            _logger.LogError(e, "Date format is not correct.");
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting sunrise/sunset.");
            return NotFound("Error getting sunrise/sunset");
        }
    }
    
    private async Task<City> GetCityFromDbOrApi(string cityName)
    {
        var city = await _cityRepository.GetByName(cityName);
        if (city == null)
        {
            var openWeatherMapData = await _coordinateDataProvider.GetCityFromOpenWeatherMap(cityName);
            city = await _jsonProcessor.ProcessWeatherApiCityStringToCity(openWeatherMapData);

            var timeZoneData = await _timeZoneProvider.GetTimeZone(city.Latitude, city.Longitude);
            var timeZone = _jsonProcessor.ProcessTimeApiResultStringForTimeZone(timeZoneData);
            if (timeZone != null)
            {
                city.TimeZone = timeZone;
            }
            
            await _cityRepository.Add(city);
        }
        _logger.LogInformation(city.TimeZone);
        return city;
    }
    
    private async Task<SunriseSunset> GetSunFromDbOrApi(City city, DateTime dateTime, string? date)
    {
        var sunriseSunset = await _sunriseSunsetRepository.GetByDateAndCity(city.Name, dateTime);
        if (sunriseSunset == null)
        {
            var sunriseSunsetData = await _sunriseSunsetProvider.GetSunriseSunset(city.Latitude, city.Longitude, date, city.TimeZone);
            sunriseSunset = _jsonProcessor.ProcessSunriseSunsetApiStringToSunriseSunset(city, dateTime, sunriseSunsetData);
            await _sunriseSunsetRepository.Add(sunriseSunset);
        }
        return sunriseSunset;
    }
}