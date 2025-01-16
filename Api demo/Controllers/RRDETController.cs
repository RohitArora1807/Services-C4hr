using Api_demo.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api_demo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RRDETController : ControllerBase
    {
        private readonly IRRDETService _rrdetService;

        public RRDETController(IRRDETService rrdetService)
        {
            _rrdetService = rrdetService;
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
    }
}
