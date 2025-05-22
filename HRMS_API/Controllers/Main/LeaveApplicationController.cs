using HRMS_API.Controllers.Security;
using HRMS_API.Models.LeaveApplication;
using HRMS_API.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using static HRMS_API.DataServices.DBHelper;

namespace HRMS_API.Controllers.Main
{
    [Route("api/LeaveApplication")]
    [ApiController]
    public class LeaveApplicationController : ControllerBase
    {
        private ILeaveApplicationService _leaveApplicationService;

        public LeaveApplicationController(ILeaveApplicationService leaveApplicationService)
        {
            _leaveApplicationService = leaveApplicationService;
        }



        [HttpGet]
        [Route("leave_type")]
        public IActionResult Post(string Year, string UserID)
        {
            try
            {
                var leaveTypeList = new List<LeaveType>();
                _leaveApplicationService.GetLeaveTypeService(UserID, Year, out leaveTypeList);

                string msg = "Data loaded";
                if (leaveTypeList.Count < 1)
                {
                    msg = "No data was found in the Database";
                }

                return Ok(new { LeaveTypeList = leaveTypeList, Message = msg});
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }



        [HttpPost]
        [Route("cal_days")]
        public IActionResult Post([FromBody] CalDaysInfo calDaysInfo)
        {
            try
            {
                var calculatedDays = new List<CalculatedDays>();
                _leaveApplicationService.GetCalDaysService(calDaysInfo.leav_catg_code, calDaysInfo.start_date, calDaysInfo.end_date, calDaysInfo.UserID, 
                    calDaysInfo.leav_type_code, calDaysInfo.Year, out calculatedDays);

                string msg = "Data loaded";
                if (calculatedDays.Count < 1)
                {
                    msg = "No data was found in the Database";
                }

                return Ok(new { CalculatedDays = calculatedDays, Message = msg });
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }


        [HttpPost]
        [Route("submit_application")]
        public IActionResult Post(List<SubmitData> submitData)
        {
            try
            {
                //string status = string.Empty, message = string.Empty;
                var status = new Response_data();
                if (submitData[0].next_supv_emcode == null)
                {
                    submitData[0].next_supv_emcode = "";
                }

                status = _leaveApplicationService.SubmitApplicationService(submitData);
                return Ok(new { ResponseCode = status.status, Message = status.message, MsgType = status.msg_type });
            }
            catch (Exception ex) { 

                return NotFound();
            }
        }
    }

}
