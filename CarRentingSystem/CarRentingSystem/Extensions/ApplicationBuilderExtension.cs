using System;
using CarRentingSystem.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using static CarRentingSystem.Core.Constants.RoleConstants;

namespace CarRentingSystem.Extensions
{
	public static class ApplicationBuilderExtension
	{
		public static async Task CreateRolesAsync(this IApplicationBuilder app)
		{
			using var scope = app.ApplicationServices.CreateAsyncScope();
			var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

			if (userManager != null && roleManager != null && await roleManager.RoleExistsAsync(AdminRole) == false)
			{
				var role = new IdentityRole(AdminRole);
				await roleManager.CreateAsync(role);

				var admin = await userManager.FindByEmailAsync("Ivan@abv");

				if (admin != null)
				{
					await userManager.AddToRoleAsync(admin, role.Name);
				}
			}
			else if (userManager != null && roleManager != null && await roleManager.RoleExistsAsync(UserRole) == false)
			{
				var role = new IdentityRole(UserRole);
				await roleManager.CreateAsync(role);
			}
		}

       

    }
}

