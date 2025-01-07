using Api_demo.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Api_demo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MWDATAController : ControllerBase
    {
        private readonly IMWDATAService _MWDATAService;

        public MWDATAController(IMWDATAService MWDATAService)
        {
            _MWDATAService = MWDATAService;
        }

        [HttpPost("GetMonthlyData")]
        public IActionResult GetMonthlyData(string stid, int catid, double monthly)
        {
            // Validate input
            if (string.IsNullOrEmpty(stid) || catid <= 0 || monthly <= 0)
            {
                return BadRequest(new { status = "error", message = "Invalid input data." });
            }

            var result = _MWDATAService.GetMonthlyData(stid, catid, monthly);

            return Ok(result);
        }
    }
}