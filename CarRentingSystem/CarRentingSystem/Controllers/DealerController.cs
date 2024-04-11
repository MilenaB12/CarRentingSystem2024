using System.Security.Claims;
using CarRentingSystem.Attributes;
using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Core.Models.Dealer;
using Microsoft.AspNetCore.Mvc;
using static CarRentingSystem.Core.Constants.MessageConstants;

namespace CarRentingSystem.Controllers
{
    public class DealerController : BaseController
    {
        private readonly IDealerService dealerService;

        public DealerController(IDealerService _dealerService)
        {
            dealerService = _dealerService;
        }

        [HttpGet]
        [NotADealer]
        public IActionResult Become()
        {
            var model = new BecomeDealerFormModel();

            return View(model);
        }

        [HttpPost]
        [NotADealer]
        public async Task<IActionResult> Become(BecomeDealerFormModel model)
        {
            if(await dealerService.UserWithPhoneNumberExistsAsync(model.PhoneNumber))
            {
                ModelState.AddModelError(nameof(model.PhoneNumber), PhoneExists);
            }

            if(await dealerService.UserHasRentsAsync(User.Id()))
            {
                this.TempData[UserErrorMessage] = "You must not have any active rents in order to become a dealer!";

                return RedirectToAction(nameof(CarController.Mine), "Car");
            }

            if(ModelState.IsValid == false)
            {
                return View(model);
            }

            try
            {
                await dealerService.CreateAsync(User.Id(), model.PhoneNumber);
            }
            catch (Exception)
            {
                this.TempData[UserErrorMessage] = "Unexpected error! Please try again later";

                return RedirectToAction("Index", "Home");
            }


            return RedirectToAction(nameof(CarController.All), "Car");
        }
    }
}

