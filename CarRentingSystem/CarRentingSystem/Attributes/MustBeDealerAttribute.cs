using System;
using System.Security.Claims;
using CarRentingSystem.Controllers;
using CarRentingSystem.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CarRentingSystem.Attributes
{
    public class MustBeDealerAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            IDealerService? dealerService = context.HttpContext.RequestServices.GetService<IDealerService>();

            if (dealerService == null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            if (dealerService != null
                && dealerService.ExistsByIdAsync(context.HttpContext.User.Id()).Result == false)
            {
                context.Result = new RedirectToActionResult(nameof(DealerController.Become), "Dealer", null);
            }
        }
    }
}

