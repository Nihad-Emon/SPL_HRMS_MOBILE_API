using HRMS_API.Models.LeaveApplication;
using static HRMS_API.DataServices.DBHelper;

namespace HRMS_API.DataServices.Interface
{
    public interface ILeaveApplicationDataService
    {
        List<LeaveType> GetLeaveTypeServiceData(string UserID, string Year);
        List<CalculatedDays> GetCalDaysService(string leav_catg_code, string start_date, string end_date, string emp_code, string leav_type_code, string Year);
        Response_data SubmitApplicationServiceData(List<SubmitData> submitData);
    }
}
