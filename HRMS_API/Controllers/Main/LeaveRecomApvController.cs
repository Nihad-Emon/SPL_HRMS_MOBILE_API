using HRMS_API.Controllers.Security;
using HRMS_API.Models;
using HRMS_API.Models.Home;
using HRMS_API.Models.RecomApprove;
using HRMS_API.Services.Class;
using HRMS_API.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static HRMS_API.DataServices.DBHelper;

namespace HRMS_API.Controllers.Main
{
    [Route("api/LeaveRecomApv")]
    [ApiController]
    public class LeaveRecomApvController : ControllerBase
    {
        private ILeaveRecomApvService _leaveRecomApvService;

        public LeaveRecomApvController(ILeaveRecomApvService leaveRecomApvService)
        {
            _leaveRecomApvService = leaveRecomApvService;
        }



        [HttpGet]
        [Route("recom_apv_list")]
        public IActionResult Post(string year, string toBrowse, string recomOrApv, string UserID)
        {
            try
            {
                var recomApvList = new List<RecomApv>();
                _leaveRecomApvService.GetRecomApvListService(year, toBrowse, recomOrApv, UserID, out recomApvList);

                string msg = "Data loaded";
                if (recomApvList.Count < 1)
                {
                    msg = "No data was found in the Database";
                }

                return Ok(new { RecomApvList = recomApvList, Message = msg });
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }




        [HttpGet]
        [Route("recom_apv_table")]
        public IActionResult Post(string year, string comp_code, string toBrowse, string recomOrApv, string UserID)
        {
            try
            {
                var recomApvTableDataList = new List<RecomApvData>();
                _leaveRecomApvService.GetRecomApvTableService(year, comp_code, toBrowse, recomOrApv, UserID, out recomApvTableDataList);

                string msg = "Data loaded";
                if (recomApvTableDataList.Count < 1)
                {
                    msg = "No data was found in the Database";
                }

                return Ok(new { RecomApvTableDataList = recomApvTableDataList, Message = msg });
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }





        [HttpGet]
        [Route("emp_wise_balance")]
        public IActionResult Get(string UserID, string Year, string Emp_code, string Comp_code)
        {
            try
            {
                var balance = new List<Balance>();
                _leaveRecomApvService.GetBalanceService(UserID, Year, Emp_code, Comp_code, out balance);

                string msg = "Data loaded";
                if (balance.Count < 1)
                {
                    msg = "No data was found in the Database";
                }

                return Ok(new { Balance = balance, Message = msg });
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }




        [HttpPost]
        [Route("submit_leave_recom_apv")]
        public IActionResult Post([FromBody] List<RecomApvData> infoList)
        {
            try
            {
                var status = new Response_data();
                status = _leaveRecomApvService.SubmitRecomApvService(infoList);

                return Ok(new { ResponseCode = status.status, Message = status.message, MsgType = status.msg_type });
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }




        [HttpPost]
        [Route("change_leave")]
        public IActionResult Post(ChangeLeave changeLeave)
        {
            try
            {
                var status = new Response_data();
                status = _leaveRecomApvService.ChangeLeaveService(changeLeave);

                return Ok(new { ResponseCode = status.status, Message = status.message, MsgType = status.msg_type });
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }
    }
}
