using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CarRentingSystem.Models;
using CarRentingSystem.Core.Models.Home;
using CarRentingSystem.Core.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace CarRentingSystem.Controllers;

public class HomeController : BaseController
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICarService carService;

    public HomeController(ILogger<HomeController> logger,
                          ICarService _carService)
    {
        _logger = logger;
        carService = _carService;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var model = await carService.LastCarsAsync();

        return View(model);
    }

    [AllowAnonymous]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

