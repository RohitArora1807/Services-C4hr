using Api_demo.Logging;
using Api_demo.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api_demo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RRDETController : ControllerBase
    {
        private readonly IRRDETService _rrdetService;
        private readonly LoggerService _logger;
        public RRDETController(IRRDETService rrdetService,LoggerService logger)
        {
            _rrdetService = rrdetService;
            _logger = logger;
        }

        [HttpGet("GetF3")]
        public IActionResult GetF3(string stid)
        {
            // Call the service to get the numerical value of F3
            var f3Value = _rrdetService.GetNumericalF3(stid);

            // Return the F3 value or NotFound if it's null
            if (f3Value == null)
            {
                return NotFound(new { message = "No numerical value found for F3." });
            }

            return Ok(f3Value);
        }
        [HttpGet("CLRAGetF1")]
        public IActionResult CLRAGetF1(string stid)
        {
            // Call the service to get the numerical value of F1
            _logger.LogError($"{DateTime.Now:yyyy - MM - dd HH: mm:ss} function called stid: {stid}");
            var f1Value = _rrdetService.CLRAGetNumericalF1(stid);

            // Return the F1 value or NotFound if it's null
            if (f1Value == null)
            {
                return NotFound(new { message = "No numerical value found for F1." });
            }

            return Ok(f1Value);
        }
    }
}
