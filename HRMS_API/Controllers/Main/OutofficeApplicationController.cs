using HRMS_API.Models.Outoffice;
using HRMS_API.Services.Class;
using HRMS_API.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using static HRMS_API.DataServices.DBHelper;

namespace HRMS_API.Controllers.Main
{
    [Route("api/OutofficeApplication")]
    [ApiController]
    public class OutofficeApplicationController : ControllerBase
    {
        private IOutofficeApplicationService _outOfficeApplicationService;

        public OutofficeApplicationController(IOutofficeApplicationService outofficeApplicationService)
        {
            _outOfficeApplicationService = outofficeApplicationService;
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
                    status = _outOfficeApplicationService.SubmitApplicationService(submitData);
                }

                return Ok(new { ResponseCode = status.status, Message = status.message, MsgType = status.msg_type });
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }



        [HttpGet]
        [Route("prev_history")]
        public IActionResult Post(string UserID, string Year)
        {
            try
            {
                var outofficeHistories = new List<OutofficeHistory>();
                _outOfficeApplicationService.GetPreviousHistoryService(UserID, Year, out outofficeHistories);

                string msg = "Data loaded";
                if (outofficeHistories.Count < 1)
                {
                    msg = "No data was found in the Database";
                }

                return Ok(new { OutofficeHistories = outofficeHistories, Message = msg });
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }




        [HttpGet]
        [Route("pnd_application_list")]
        public IActionResult GetPndAplnList(string UserID, string Year)
        {
            try
            {
                var pndLeaveApplicationList = new List<PndLeaveApplication>();
                _outOfficeApplicationService.GetPndAplnListService(UserID, Year, out pndLeaveApplicationList);

                string msg = "Data loaded";
                if (pndLeaveApplicationList.Count < 1)
                {
                    msg = "No data was found in the Database";
                }

                return Ok(new { PndLeaveApplicationList = pndLeaveApplicationList, Message = msg });
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }
    }
}
