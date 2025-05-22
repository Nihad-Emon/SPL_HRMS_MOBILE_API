using HRMS_API.Models.EarlyOutApplication;
using HRMS_API.Services.Class;
using HRMS_API.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static HRMS_API.DataServices.DBHelper;

namespace HRMS_API.Controllers.Main
{
    [Route("api/EarlyOutApplication")]
    [ApiController]
    public class EarlyOutApplicationController : ControllerBase
    {

        private IEarlyOutApplicationService _earlyOutApplicationService; 

        public EarlyOutApplicationController(IEarlyOutApplicationService earlyOutApplicationService)
        {
            _earlyOutApplicationService = earlyOutApplicationService;
        }


        [HttpGet]
        [Route("early_out_cause")]
        public IActionResult Get(string UserID)
        {
            try
            {
                var earltOutCauseList = new List<EarlyOutCause>();
                _earlyOutApplicationService.GetEarlyOutCauseService(UserID, out earltOutCauseList);

                string msg = "Data loaded";
                if (earltOutCauseList.Count < 1)
                {
                    msg = "No data was found in the Database";
                }

                return Ok(new { LeaveTypeList = earltOutCauseList, Message = msg });
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
                if (submitData.NEXT_SUPV_EMP_CODE == null || submitData.NEXT_SUPV_EMP_CODE == "")
                {
                    status.status = "0";
                    status.message = "Next Supervisor is missing";
                    status.msg_type = "E";
                }
                else
                {
                    status = _earlyOutApplicationService.SubmitApplicationService(submitData);
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
        public IActionResult Get(string UserID, string Year)
        {
            try
            {
                var pndEarlyOutApplicationList = new List<PndEarlyOutApplication>();
                _earlyOutApplicationService.GetPndEarlyOutApplicationListService(UserID, Year, out pndEarlyOutApplicationList);

                string msg = "Data loaded";
                if (pndEarlyOutApplicationList.Count < 1)
                {
                    msg = "No data was found in the Database";
                }

                return Ok(new { PndLeaveApplicationList = pndEarlyOutApplicationList, Message = msg });
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }




        [HttpGet]
        [Route("prev_history")]
        public IActionResult GetPreviousHistory(string UserID, string Year)
        {
            try
            {
                var earlyOutHistory = new List<EarlyOutHistory>();
                _earlyOutApplicationService.GetPreviousHistoryService(UserID, Year, out earlyOutHistory);

                string msg = "Data loaded";
                if (earlyOutHistory.Count < 1)
                {
                    msg = "No data was found in the Database";
                }

                return Ok(new { LateComingHistory = earlyOutHistory, Message = msg });
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }
    }
}
