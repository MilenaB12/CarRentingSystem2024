using System;
using CarRentingSystem.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CarRentingSystem.Areas.Admin.Controllers
{
	public class UserController : AdminController
	{
		private readonly IUserService userService;

		public UserController(IUserService _userService)
		{
			userService = _userService;
		}

		public async Task<IActionResult> All()
		{
            var model = await userService.AllAsync();

            return View(model);
		}
	}

}

