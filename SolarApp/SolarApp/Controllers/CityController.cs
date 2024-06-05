using Microsoft.AspNetCore.Mvc;
using SolarApp.Services.JsonProcessor;
using SolarApp.Services.Providers.CoordinateProvider;
using SolarApp.Services.Repository;

namespace SolarApp.Controllers;

public class CityController : Controller
{
    private readonly ILogger<CityController> _logger;
    private readonly ICityRepository _cityRepository;
    private readonly ICoordinateProvider _coordinateProvider;
    private readonly IJsonProcessor _jsonProcessor;

    public CityController(ILogger<CityController> logger, ICityRepository cityRepository, 
        ICoordinateProvider coordinateProvider, IJsonProcessor jsonProcessor)
    {
        _logger = logger;
        _cityRepository = cityRepository;
        _coordinateProvider = coordinateProvider;
        _jsonProcessor = jsonProcessor;
    }
    public async Task<IActionResult> Index()
    {
        try
        {
            var cities = await _cityRepository.GetAll();
            return View(cities);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured while trying to list all cities.");
            return StatusCode(500, "An error occured while trying to list all cities.");
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> AddCity(string name, string country)
    {
        _logger.LogInformation(country);
        try
        {
            var cityToAdd = await _cityRepository.GetByNameAndCountry(name, country);
            if (cityToAdd == null)
            {
                var openWeatherMapData = await _coordinateProvider.GetCityFromOpenWeatherMap(name, country);
                cityToAdd = await _jsonProcessor.ProcessWeatherApiCityStringToCity(openWeatherMapData);
                await _cityRepository.Add(cityToAdd);
            }
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured while trying to get the city.");
            return StatusCode(500, "An error occured while trying to get the city.");
        }
    }
    
    
}