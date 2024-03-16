using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Core.Models.Dealer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentingSystem.Controllers
{
    [Authorize]
    public class DealerController : Controller
    {
        private readonly IDealerService IDealerService;

        public DealerController(IDealerService )
        {

        }

        [HttpGet]
        public async Task<IActionResult> Become()
        {
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

