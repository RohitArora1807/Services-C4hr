using Api_demo.Logging;
using Microsoft.AspNetCore.Mvc;

namespace Api_demo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ObligationController : ControllerBase
    {
        private readonly LoggerService _logger;

        public ObligationController(LoggerService logger) 
        {
             _logger=logger;
    }
        public IActionResult Index()
        {
            return Ok("success");
        }
    }
}
