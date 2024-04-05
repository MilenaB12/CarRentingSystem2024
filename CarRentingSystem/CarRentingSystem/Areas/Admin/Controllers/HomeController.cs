using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CarRentingSystem.Areas.Admin.Controllers
{
    public class HomeController : AdminController
    {
        public IActionResult Board()
        {
            return View();
        }

        public IActionResult ForApproval()
        {
            return View();
        }
    }
}

