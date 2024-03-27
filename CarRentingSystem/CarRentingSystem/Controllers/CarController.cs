using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CarRentingSystem.Attributes;
using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Core.Exceptions;
using CarRentingSystem.Core.Models.Car;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarRentingSystem.Controllers
{
    public class CarController : BaseController
    {
        private readonly ICarService carService;

        private readonly IDealerService dealerService;

        private readonly ILogger logger;


        public CarController(
            ICarService _carService,
            IDealerService _dealerService,
            ILogger<CarController> _logger)
        {
            carService = _carService;
            dealerService = _dealerService;
            logger = _logger;
        }

        [AllowAnonymous]
        [HttpGet] 
        public async Task<IActionResult> All([FromQuery]AllCarsQueryModel query)
        {
            var model = await carService.AllAsync(
                query.Category,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                query.CarsPerPage);

            query.TotalCarsCount = model.TotalCarsCount;
            query.Cars = model.Cars;
            query.Categories = await carService.AllCategoriesNamesAsync ();

            return View(query);
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            var userId = User.Id();
            IEnumerable<CarServiceModel> model;

            if (await dealerService.ExistsByIdAsync(userId))
            {
                int dealerId = await dealerService.GetDealerIdAsync(userId) ?? 0;
                model = await carService.AllCarsByDealerIdAsync(dealerId);
            }
            else
            {
                model = await carService.AllCarsByUserId(userId);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (await carService.ExistsAsync(id) == false)
            {
                return BadRequest();
            }

            var model = await carService.CarDetailsByIdAsync(id);

            return View(model);
        }

        [HttpGet]
        [MustBeDealer]
        public async Task<IActionResult> Add()
        {
            var model = new CarFormModel()
            {
                Categories = await carService.AllCategoriesAsync(),
                Brands = await carService.AllBrandsAsync()
            };

            return View(model);
        }

        [HttpPost]
        [MustBeDealer]
        public async Task<IActionResult> Add(CarFormModel model)
        {
            if(await carService.CategoryExistsAsync(model.CategoryId) == false)
            {
                ModelState.TryAddModelError(nameof(model.CategoryId), "Category does not exist");
            }

            if (await carService.BrandExistsAsync(model.BrandId) == false)
            {
                ModelState.TryAddModelError(nameof(model.BrandId), "Brand does not exist");
            }

            if (ModelState.IsValid == false)
            {
                model.Categories = await carService.AllCategoriesAsync();
                model.Brands = await carService.AllBrandsAsync();

                return View(model);
            }

            int? dealerId = await dealerService.GetDealerIdAsync(User.Id());

            int newCarId = await carService.CreateAsync(model, dealerId ?? 0);

            return RedirectToAction(nameof(Details), new {id = newCarId });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (await carService.ExistsAsync(id) == false)
            {
                return BadRequest();
            }

            if (await carService.HasDealerWithIdAsync(id, User.Id()) == false)
            {
                return Unauthorized();
            }

            var model = await carService.GetCarFormModelByIdAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CarFormModel model)
        {
            if (await carService.ExistsAsync(id) == false)
            {
                return BadRequest();
            }

            if (await carService.HasDealerWithIdAsync(id, User.Id()) == false)
            {
                return Unauthorized();
            }

            if (await carService.CategoryExistsAsync(model.CategoryId) == false)
            {
                ModelState.TryAddModelError(nameof(model.CategoryId), "Category does not exist");
            }

            if (await carService.BrandExistsAsync(model.BrandId) == false)
            {
                ModelState.TryAddModelError(nameof(model.BrandId), "Brand does not exist");
            }

            if (ModelState.IsValid == false)
            {
                model.Categories = await carService.AllCategoriesAsync();
                model.Brands = await carService.AllBrandsAsync();

                return View(model);
            }

            await carService.EditAsync(id, model);

            return RedirectToAction(nameof(Details), new { id = id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (await carService.ExistsAsync(id) == false)
            {
                return BadRequest();
            }

            if (await carService.HasDealerWithIdAsync(id, User.Id()) == false)
            {
                return Unauthorized();
            }

            var car = await carService.CarDetailsByIdAsync(id);

            var model = new CarDetailsViewModel()
            {
                Id = id,
                Color = car.Color,
                ImageUrl = car.ImageUrl,
                Brand = car.Brand
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(CarDetailsViewModel model)
        {
            if (await carService.ExistsAsync(model.Id) == false)
            {
                return BadRequest();
            }

            if (await carService.HasDealerWithIdAsync(model.Id, User.Id()) == false)
            {
                return Unauthorized();
            }

            await carService.DeleteAsync(model.Id);

            return RedirectToAction(nameof(All));

        }

        [HttpPost]
        public async Task<IActionResult> Rent(int id)
        {
            if (await carService.ExistsAsync(id) == false)
            {
                return BadRequest();
            }

            if (await dealerService.ExistsByIdAsync(User.Id()))
            {
                return Unauthorized();
            }

            if (await carService.IsRentedAsync(id))
            {
                return BadRequest();
            }

            await carService.RentAsync(id, User.Id());

            return RedirectToAction(nameof(Mine));
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            if (await carService.ExistsAsync(id) == false)
            {
                return BadRequest();
            }

            try
            {
                await carService.LeaveAsync(id, User.Id());
            }
            catch (UnauthorizedActionException uae)
            {
                logger.LogError(uae, "CarController/Leave");

                return Unauthorized();
            }

            return RedirectToAction(nameof(Mine));
        }
    }
}

