using HRMS_API.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS_API.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        readonly ITestService _testService;

        public TestController(ITestService testService){ 
            _testService = testService;
        }


        // Test End-Point
        [HttpGet]
        [Route("hi")]
        public async Task<ActionResult> GetLeaveYear()
        {
            try
            {
                var leaveYearList = _testService.GetTestService();
                
                return Ok(new { LeaveYearList = leaveYearList, Message = "It's working!" });
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return NotFound(ex);
            }
        }
    }
}
