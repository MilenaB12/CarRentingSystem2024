using System;
using CarRentingSystem.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CarRentingSystem.Controllers
{
    [Route("api/statistic")]
    [ApiController]

    public class StatisticApi : ControllerBase
    {
        private readonly IStatisticService statisticService;

        public StatisticApi(IStatisticService _statisticService)
        {
            statisticService = _statisticService;
        }

        [HttpGet]

        public async Task<IActionResult> Get()
        {
            var result = await statisticService.TotalAsync();

            return Ok(result);
        }
    }
}

