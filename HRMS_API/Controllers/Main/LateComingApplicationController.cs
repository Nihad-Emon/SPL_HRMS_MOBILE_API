using HRMS_API.Models.LateComingApplication;
using HRMS_API.Services.Class;
using HRMS_API.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static HRMS_API.DataServices.DBHelper;

namespace HRMS_API.Controllers.Main
{
    [Route("api/LateComingApplication")]
    [ApiController]
    public class LateComingApplicationController : ControllerBase
    {
        private ILateComingApplicationService _lateComingApplicationService;
        
        public LateComingApplicationController(ILateComingApplicationService lateComingApplicationService)
        {
            _lateComingApplicationService = lateComingApplicationService;
        }




        [HttpGet]
        [Route("late_cause")]
        public IActionResult Get(string UserID)
        {
            try
            {
                var lateCauseList = new List<LateCause>();
                _lateComingApplicationService.GetLateCauseService(UserID, out lateCauseList);

                string msg = "Data loaded";
                if (lateCauseList.Count < 1)
                {
                    msg = "No data was found in the Database";
                }

                return Ok(new { LeaveTypeList = lateCauseList, Message = msg });
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }



        [HttpPost]
        [Route("submit_application")]
        public IActionResult Post(SubmitData submitData)
        {
            try
            {
                //string status = string.Empty, message = string.Empty;
                var status = new Response_data();
                if (submitData.next_supv_emcode == null || submitData.next_supv_emcode == "")
                {
                    status.status = "0";
                    status.message = "Next Supervisor is missing";
                    status.msg_type = "E";
                }
                else
                {
                    status = _lateComingApplicationService.SubmitApplicationService(submitData);
                }

                return Ok(new { ResponseCode = status.status, Message = status.message, MsgType = status.msg_type });
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }



        [HttpGet]
        [Route("pnd_application_list")]
        public IActionResult GetPndLateApplicationList(string UserID, string Year)
        {
            try
            {
                var pndLateApplicationList = new List<PndLateApplication>();
                _lateComingApplicationService.GetPndLateApplicationListService(UserID, Year, out pndLateApplicationList);

                string msg = "Data loaded";
                if (pndLateApplicationList.Count < 1)
                {
                    msg = "No data was found in the Database";
                }

                return Ok(new { PndLeaveApplicationList = pndLateApplicationList, Message = msg });
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }



        [HttpGet]
        [Route("prev_history")]
        public IActionResult Post(string UserID, string Year)
        {
            try
            {
                var lateComingHistory = new List<LateComingHistory>();
                _lateComingApplicationService.GetPreviousHistoryService(UserID, Year, out lateComingHistory);

                string msg = "Data loaded";
                if(lateComingHistory.Count < 1)
                {
                    msg = "No data was found in the Database";
                }

                return Ok(new { LateComingHistory = lateComingHistory, Message = msg });
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }
    }
}
