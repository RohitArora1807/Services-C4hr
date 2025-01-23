using Api_demo.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api_demo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InfoController : ControllerBase
    {
        private readonly IHelloService _helloService;

        public InfoController(IHelloService helloService)
        {
            _helloService = helloService;
        }

        // GET: api/hello
        [HttpGet]
        public IActionResult GetHello()
        {
            var message = _helloService.GetHelloMessage();
            return Ok(message);
        }

        //Post
        [HttpPost]
        public IActionResult PostHello(int id) {
            var response = _helloService.ProcessHello(id);
            return Ok(response);
        }
    }
}
