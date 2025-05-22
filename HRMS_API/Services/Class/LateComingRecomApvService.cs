using HRMS_API.DataServices;
using HRMS_API.DataServices.Class;
using HRMS_API.DataServices.Interface;
using HRMS_API.Models.LateComingRecomApv;
using HRMS_API.Services.Interface;
using System.ComponentModel.DataAnnotations;
using static HRMS_API.DataServices.DBHelper;

namespace HRMS_API.Services.Class
{
    public class LateComingRecomApvService : ILateComingRecomApvService
    {
        private readonly ILateComingRecomApvDataService _lateComingRecomApvDataService;

        public LateComingRecomApvService(ILateComingRecomApvDataService lateComingRecomApvDataService)
        {
            _lateComingRecomApvDataService = lateComingRecomApvDataService;
        }

        public ValidationResult GetRecomApvListService(string year, string toBrowse, string recomOrApv, string UserID, out List<RecomApv> recomApvList)
        {
            recomApvList = _lateComingRecomApvDataService.GetRecomApvListServiceData(year, toBrowse, recomOrApv, UserID);
            return ValidationResult.Success;
        }

        public ValidationResult GetRecomApvTableService(string year, string comp_code, string toBrowse, string recomOrApv, string UserID, out List<RecomApvData> recomApvTableDataList)
        {
            recomApvTableDataList = _lateComingRecomApvDataService.GetRecomApvTableServiceData(year, comp_code, toBrowse, recomOrApv, UserID);
            return ValidationResult.Success;
        }

        public Response_data SubmitRecomApvService(List<RecomApvData> infoList)
        {
            return _lateComingRecomApvDataService.SubmitRecomApvServiceData(infoList);
        }
    }
}
