using HRMS_API.Models;
using HRMS_API.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static HRMS_API.DataServices.DBHelper;

namespace HRMS_API.Controllers.Security
{
    [Route("api/ChangePass")]
    [ApiController]
    public class ChangePasswordController : ControllerBase
    {
        private IChangePassService _changePassService;

        public ChangePasswordController(IChangePassService changePassService)
        {
            _changePassService = changePassService;
        }


        [HttpPost]
        [Route("changepassword")]
        public IActionResult Post(ChangeInfo changeInfo)
        {
            try
            {
                SecUser objUser = new SecUser();
                var status = new Response_data();
                string action = "changePassword";
                objUser.Set_by = changeInfo.User_id; 
                objUser.User_id = changeInfo.User_id;
                objUser.Mod_by = changeInfo.User_id;

                objUser.User_name = changeInfo.User_name; 
                objUser.User_pass = changeInfo.password;

                status = _changePassService.ChangePassService(objUser, changeInfo.oldPassword, action);

                return Ok(new { ResponseCode = status.status, Message = status.message, MsgType = status.msg_type });
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }
    }
}
