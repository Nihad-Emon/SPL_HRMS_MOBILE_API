using HRMS_API.Models.Reports;
using HRMS_API.Services.Class;
using HRMS_API.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HRMS_API.Controllers.Main
{
    [Route("api/Reports")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private IReportsService _reportsService;
        public static string msgSuccess = "Data loaded";
        public static string msgError = "No data was found in the Database";

        public ReportsController(IReportsService reportsService)
        {
            _reportsService = reportsService;
        }


        [HttpGet]
        [Route("job_card")]
        public IActionResult Get(string UserID, string FromDate, string ToDate)
        {
            try
            {
                var jobCard = new JobCard();
                _reportsService.GetJobCardService(UserID, FromDate, ToDate, out List<JobCard> jobcard);

                string msg = msgSuccess;
                if (jobcard.Count < 1)
                {
                    msg = msgError;
                }

                return Ok(new { JobCard = jobcard, Message = msg });
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return NotFound(ex);
            }
        }
    }
}
