using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Core.Services;
using CarRentingSystem.Infrastructure.Data;
using CarRentingSystem.Infrastructure.Data.Common;
using CarRentingSystem.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class ServiceCollectionExtension
	{
        public static IServiceCollection AddAplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IDealerService, DealerService>();
            services.AddScoped<IStatisticService, StatisticService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRentService, RentService>();

            return services;
        }
        
        public static IServiceCollection AddAplicationDbContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<CarRentingDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IRepository, Repository>();

            services.AddDatabaseDeveloperPageExceptionFilter();

            return services;
        }


        public static IServiceCollection AddAplicationIdentity(this IServiceCollection services, IConfiguration config)
        {
            services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
            })
             .AddRoles<IdentityRole>()
             .AddEntityFrameworkStores<CarRentingDbContext>();

            return services;
        }
    }
}

