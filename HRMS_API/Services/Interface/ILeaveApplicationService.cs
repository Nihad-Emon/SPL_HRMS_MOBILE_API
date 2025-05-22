using HRMS_API.Models.LeaveApplication;
using System.ComponentModel.DataAnnotations;
using static HRMS_API.DataServices.DBHelper;

namespace HRMS_API.Services.Interface
{
    public interface ILeaveApplicationService
    {
        ValidationResult GetLeaveTypeService(string UserID, string Year, out List<LeaveType> LeaveTypeList);
        ValidationResult GetCalDaysService(string leav_catg_code, string start_date, string end_date, string emp_code, string leav_type_code, string Year, out List<CalculatedDays>calculatedDays);
        Response_data SubmitApplicationService(List<SubmitData> submitData);
    }
}
