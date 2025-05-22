using HRMS_API.Controllers.Security;
using HRMS_API.Models.Home;
using HRMS_API.Models.LateComingApplication;
using HRMS_API.Services.Class;
using HRMS_API.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS_API.Controllers.Main
{
    [Route("api/Home")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private IHomeService _homeService;
        public static string msgSuccess = "Data loaded";
        public static string msgError = "No data was found in the Database";

        public HomeController(IHomeService homeService) {

            _homeService = homeService;
        }


        [HttpGet]
        [Route("leavelyear")]
        public IActionResult Get(string UserID)
        {
            try
            {
                var leaveYearList = new List<LeaveYear>();
                _homeService.GetLeavelYearService(UserID, out leaveYearList);

                string msg = msgSuccess;
                if (leaveYearList.Count < 1)
                {
                    msg = msgError;
                }

                return Ok(new { LeaveYearList = leaveYearList, Message = msg });
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return NotFound(ex);
            }
        }



        [HttpGet]
        [Route("balance")]
        public IActionResult Post(string UserID, string Year)
        {
            try
            {
                var balance = new List<Balance>();
                _homeService.GetBalanceService(UserID, Year, out balance);

                string msg = msgSuccess;
                if (balance.Count < 1)
                {
                    msg = msgError;
                }

                return Ok(new { Balance = balance, Message = msg });
            }
            catch (Exception ex) {
                return NotFound(ex);
            }
        }


        [HttpGet]
        [Route("get_leave_count")]
        public IActionResult GetLeaveCount(string Year, string UserID)
        {
            try
            {
                var ToBrowse = "";
                var total = new List<Total>();
                _homeService.GetLeaveCountService(Year, ToBrowse, UserID, out total);

                string msg = msgSuccess;
                if (total.Count < 1)
                {
                    msg = msgError;
                }

                return Ok(new { LeaveCount = total, Message = msg });
            }
            catch(Exception ex)
            {
                return NotFound(ex);
            }
        }



        [HttpGet]
        [Route("pnd_application_count")]
        public IActionResult GetPendingAplnCount(string Year, string UserID)
        {
            try
            {
                int pending = 0;
                _homeService.GetPendingAplnCountService(UserID, Year, out pending);

                string msg = msgSuccess;
                if (pending < 1)
                {
                    msg = msgError;
                }

                return Ok(new { Pending = pending, Message = msg });
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }



        [HttpGet]
        [Route("pnd_application_list")]
        public IActionResult GetPndLeaveApplicationList(string Year, string UserID)
        {
            try
            {

                var pndLeaveApplicationList = new List<PndLeaveApplication>();
                _homeService.GetPndLeaveApplicationListService(UserID, Year, out pndLeaveApplicationList);

                string msg = msgSuccess;
                if (pndLeaveApplicationList.Count < 1)
                {
                    msg = msgError;
                }

                return Ok(new { PndLeaveApplicationList = pndLeaveApplicationList, Message = msg });
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }




        [HttpGet]
        [Route("leave_history")]
        public IActionResult GetLeaveHistory(string Year, string UserID, string comp_code)
        {
            try
            {
                var leaveHistoryList = new List<LeaveHistory>();
                if (comp_code == "fu")
                {
                    comp_code = null;
                }
                if (!string.IsNullOrEmpty(comp_code))
                {
                    
                    _homeService.GetLeaveHistoryServiceForOthers(UserID, Year, comp_code, out leaveHistoryList);
                }
                else
                {
                    _homeService.GetLeaveHistoryService(UserID, Year, out leaveHistoryList);
                }                

                string msg = msgSuccess;
                if (leaveHistoryList.Count < 1)
                {
                    msg = msgError;
                }

                return Ok(new { LeaveHistoryList = leaveHistoryList, Message = msg });
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return NotFound(ex);
            }
        }




        [HttpGet]
        [Route("absent_list")]
        public IActionResult GetAbsentList(string Year, string UserID)
        {
            try
            {

                var absentList = new List<Absent>();
                _homeService.GetAbsentListService(UserID, Year, out absentList);

                string msg = msgSuccess;
                if (absentList.Count < 1)
                {
                    msg = msgError;
                }

                return Ok(new { AbsentList = absentList, Message = msg });
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return NotFound(ex);
            }
        }



        [HttpGet]
        [Route("ip_phone_list")]
        public IActionResult GetIpPhoneList(string UserID)
        {
            try
            {
                var ipPhoneList = new List<IpPhone>();
                _homeService.GetIpPhoneListService(UserID, out ipPhoneList);

                string msg = msgSuccess;
                if(ipPhoneList.Count < 1)
                {
                    msg = msgError;
                }

                return Ok(new { IpPhoneList = ipPhoneList, Message = msg });
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }

        }


        [HttpGet]
        [Route("holiday_list")]
        public IActionResult GetHolidayList(string UserID)
        {
            try
            {
                var holidayList = new List<Holiday>();
                _homeService.GetHolidayListService(UserID, out holidayList);

                string msg = msgSuccess;
                if (holidayList.Count < 1)
                {
                    msg = msgError;
                }
                return Ok(new { HolidayList = holidayList, Message = msg });
            }
            catch(Exception ex) { 
                return NotFound(ex);
            } 
        }


        ///
    }
}
