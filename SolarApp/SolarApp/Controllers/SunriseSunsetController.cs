using Microsoft.AspNetCore.Mvc;
using SolarApp.Services.JsonProcessor;
using SolarApp.Services.Providers.CoordinateProvider;
using SolarApp.Services.Providers.SunriseSunsetProvider;
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
    // GET
    public IActionResult Index()
    {
        return View();
    }
}