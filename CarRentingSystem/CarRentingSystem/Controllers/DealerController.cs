using System.Security.Claims;
using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Core.Models.Dealer;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Become()
        {
            if(await dealerService.ExistsByIdAsync(User.Id()))
            {
                return BadRequest();
            }

            var model = new BecomeDealerFormModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Become(BecomeDealerFormModel model)
        {
            return RedirectToAction(nameof(CarController.All), "Car");
        }
    }
}

