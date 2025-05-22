using HRMS_API.DataServices.Interface;
using HRMS_API.Models.LeaveApplication;
using HRMS_API.Services.Interface;
using System.ComponentModel.DataAnnotations;
using static HRMS_API.DataServices.DBHelper;

namespace HRMS_API.Services.Class
{
    public class LeaveApplicationService : ILeaveApplicationService
    {
        private readonly ILeaveApplicationDataService _leaveApplicationDataService;

        public LeaveApplicationService(ILeaveApplicationDataService leaveApplicationDataService)
        {
            _leaveApplicationDataService = leaveApplicationDataService;
        }        


        public ValidationResult GetLeaveTypeService(string UserID, string Year, out List<LeaveType> LeaveTypeList)
        {
            LeaveTypeList = _leaveApplicationDataService.GetLeaveTypeServiceData(UserID, Year);
            return ValidationResult.Success;
        }


        public ValidationResult GetCalDaysService(string leav_catg_code, string start_date, string end_date, string emp_code, string leav_type_code, string Year, out List<CalculatedDays> calculatedDays)
        {
            calculatedDays = _leaveApplicationDataService.GetCalDaysService(leav_catg_code, start_date, end_date, emp_code, leav_type_code, Year);
            return ValidationResult.Success;
        }


        public Response_data SubmitApplicationService(List<SubmitData> submitData)
        {
            return _leaveApplicationDataService.SubmitApplicationServiceData(submitData);
        }

    }
}
