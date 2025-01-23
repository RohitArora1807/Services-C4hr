//using Microsoft.AspNetCore.Mvc;
//using Api_demo.Services;

//namespace Api_demo.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class ActController : ControllerBase
//    {
//        private readonly IActService _actService;

//        public ActController(IActService actService)
//        {
//            _actService = actService;
//        }

//        [HttpGet("GetLName")]
//        public IActionResult GetLName(string stcode, string aapl)
//        {
//            if (string.IsNullOrWhiteSpace(stcode) || string.IsNullOrWhiteSpace(aapl))
//                return BadRequest("Both STCODE and AAPL are required.");

//            var result = _actService.GetLNameByStcodeAndAapl(stcode, aapl);
//            if (result == null)
//                return NotFound("No record found for the given STCODE and AAPL.");

//            return Ok(result);
//        }
//    }
//}


using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Api_demo.Services;

namespace Api_demo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActController : ControllerBase
    {
        private readonly IActService _actService;

        public ActController(IActService actService)
        {
            _actService = actService;
        }

        [HttpGet("GetActsByTypeAndState")]
        public IActionResult GetActsByTypeAndState(string type, string state)
        {
            if (string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(state))
                return BadRequest("Type and State are required parameters.");

            try
            {
                var acts = _actService.GetActsByTypeAndState(type.Trim(), state.Trim());
                if (acts == null || acts.Count == 0)
                    return NotFound("No records found for the given criteria.");

                return Ok(acts);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
