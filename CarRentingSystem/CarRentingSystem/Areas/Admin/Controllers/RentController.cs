using System;
using CarRentingSystem.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CarRentingSystem.Areas.Admin.Controllers
{
	public class RentController : AdminController
	{
		private readonly IRentService rentService;

		public RentController(IRentService _rentService)
		{
			rentService = _rentService;
		}

		public async Task<IActionResult> AllRents()
		{
			var model = await rentService.AllRentAsync();

			return View(model);
		}
	}
}

