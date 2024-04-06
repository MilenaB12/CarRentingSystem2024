using System;
using CarRentingSystem.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CarRentingSystem.Areas.Admin.Controllers
{
	public class CarController : AdminController
	{
		private readonly ICarService carService;

        public CarController(ICarService _carService)
		{
			carService = _carService;
		}

		[HttpGet]
		public async Task<IActionResult> Approve()
		{
			var model = await carService.GetApprovedAsync();

			return View(model);
		}

		[HttpPost]
        public async Task<IActionResult> Approve(int carId)
        {
			await carService.ApproveCarAsync(carId);

            return RedirectToAction(nameof(Approve));
        }
    }
}

