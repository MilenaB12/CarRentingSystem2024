using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CarRentingSystem.Models;
using CarRentingSystem.Core.Models.Home;
using CarRentingSystem.Core.Contracts;

namespace CarRentingSystem.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICarService carService;

    public HomeController(ILogger<HomeController> logger,
                          ICarService _carService)
    {
        _logger = logger;
        carService = _carService;
    }

    public async Task<IActionResult> Index()
    {
        var model = await carService.LastCars();

        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

