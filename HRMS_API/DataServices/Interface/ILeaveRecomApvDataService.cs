using HRMS_API.Models.Home;
using HRMS_API.Models.RecomApprove;
using static HRMS_API.DataServices.DBHelper;

namespace HRMS_API.DataServices.Interface
{
    public interface ILeaveRecomApvDataService
    {
        List<RecomApv> GetRecomApvListServiceData(string year, string toBrowse, string recomOrApv, string UserID);
        List<RecomApvData> GetRecomApvTableServiceData(string year, string comp_code, string toBrowse, string recomOrApv, string UserID);
        List<Balance> GetBalanceServiceData(string UserID, string Year, string Emp_code, string Comp_code);
        Response_data SubmitRecomApvServiceData(List<RecomApvData> infoList);
        Response_data ChangeLeaveServiceData(ChangeLeave changeLeave);
    }
}
