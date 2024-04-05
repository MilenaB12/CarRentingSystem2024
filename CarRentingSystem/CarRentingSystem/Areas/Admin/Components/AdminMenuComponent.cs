using System;
using Microsoft.AspNetCore.Mvc;

namespace CarRentingSystem.Areas.Admin.Components
{
	public class AdminMenuComponent : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return await Task.FromResult<IViewComponentResult>(View());
		}
	}
}

