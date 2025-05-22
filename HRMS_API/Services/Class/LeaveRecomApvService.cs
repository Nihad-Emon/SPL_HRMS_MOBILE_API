using HRMS_API.DataServices.Interface;
using HRMS_API.Models.Home;
using HRMS_API.Models.RecomApprove;
using HRMS_API.Services.Interface;
using System.ComponentModel.DataAnnotations;
using static HRMS_API.DataServices.DBHelper;

namespace HRMS_API.Services.Class
{
    public class LeaveRecomApvService : ILeaveRecomApvService
    {
        private readonly ILeaveRecomApvDataService _leaveRecomApvDataService;

        public LeaveRecomApvService(ILeaveRecomApvDataService leaveRecomApvDataService)
        {
            _leaveRecomApvDataService = leaveRecomApvDataService;
        }        

        public ValidationResult GetRecomApvListService(string year, string toBrowse, string recomOrApv, string UserID, out List<RecomApv> recomApvList)
        {
            recomApvList = _leaveRecomApvDataService.GetRecomApvListServiceData(year, toBrowse, recomOrApv, UserID);
            return ValidationResult.Success;
        }

        public ValidationResult GetRecomApvTableService(string year, string comp_code, string toBrowse, string recomOrApv, string UserID,
            out List<RecomApvData> recomApvTableDataList)
        {
            recomApvTableDataList = _leaveRecomApvDataService.GetRecomApvTableServiceData(year, comp_code, toBrowse, recomOrApv, UserID);
            return ValidationResult.Success;
        }

        public ValidationResult GetBalanceService(string UserID, string Year, string Emp_code, string Comp_code, out List<Balance> balance)
        {
            balance = _leaveRecomApvDataService.GetBalanceServiceData(UserID, Year, Emp_code, Comp_code);
            return ValidationResult.Success;
        }

        public Response_data SubmitRecomApvService(List<RecomApvData> infoList)
        {
            return _leaveRecomApvDataService.SubmitRecomApvServiceData(infoList);
        }


        public Response_data ChangeLeaveService(ChangeLeave changeLeave)
        {
            return _leaveRecomApvDataService.ChangeLeaveServiceData(changeLeave);
        }        
    }
}
