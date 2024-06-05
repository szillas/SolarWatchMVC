using Microsoft.AspNetCore.Mvc;

namespace SolarApp.Controllers;

public class CityController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}