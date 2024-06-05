using Microsoft.AspNetCore.Mvc;
using SolarApp.Services.Providers.CoordinateProvider;
using SolarApp.Services.Repository;

namespace SolarApp.Controllers;

public class CityController : Controller
{
    private readonly ILogger<CityController> _logger;
    private readonly ICityRepository _cityRepository;
    private readonly ICoordinateProvider _coordinateProvider;

    public CityController(ILogger<CityController> logger, ICityRepository cityRepository, ICoordinateProvider coordinateProvider)
    {
        _logger = logger;
        _cityRepository = cityRepository;
        _coordinateProvider = coordinateProvider;
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
    public async Task<IActionResult> GetCity(string country, string city)
    {
        try
        {
            var cityToAdd = await _cityRepository.GetByNameAndCountry(city, country);
            if (cityToAdd == null)
            {
                return NotFound("City is not found!");
            }
            return Ok(cityToAdd);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured while trying to get the city.");
            return StatusCode(500, "An error occured while trying to get the city.");
        }
    }
}