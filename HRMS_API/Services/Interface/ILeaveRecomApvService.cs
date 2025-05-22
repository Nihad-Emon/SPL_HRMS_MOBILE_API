using HRMS_API.Models.Home;
using HRMS_API.Models.RecomApprove;
using System.ComponentModel.DataAnnotations;
using static HRMS_API.DataServices.DBHelper;

namespace HRMS_API.Services.Interface
{
    public interface ILeaveRecomApvService
    {
        ValidationResult GetRecomApvListService(string year, string toBrowse, string recomOrApv, string UserID, out List<RecomApv> recomApvList);
        ValidationResult GetRecomApvTableService(string year, string comp_code, string toBrowse, string recomOrApv, string UserID, out List<RecomApvData> recomApvTableDataList);
        ValidationResult GetBalanceService(string UserID, string Year, string Emp_code, string Comp_code, out List<Balance> balance);
        Response_data SubmitRecomApvService(List<RecomApvData> infoList);
        Response_data ChangeLeaveService(ChangeLeave changeLeave);
    }
}
