using HRMS_API.DataServices.Class;
using HRMS_API.DataServices.Interface;
using HRMS_API.Models.EarlyOutRecomApv;
using HRMS_API.Services.Interface;
using System.ComponentModel.DataAnnotations;
using static HRMS_API.DataServices.DBHelper;

namespace HRMS_API.Services.Class
{
    public class EarlyOutRecomApvService : IEarlyOutRecomApvService
    {
        private readonly IEarlyOutRecomApvDataService _earlyOutRecomApvDataService;

        public EarlyOutRecomApvService(IEarlyOutRecomApvDataService earlyOutRecomApvDataService)
        {
            _earlyOutRecomApvDataService = earlyOutRecomApvDataService;
        }

        public ValidationResult GetRecomApvListService(string year, string toBrowse, string recomOrApv, string UserID, out List<RecomApv> recomApvList)
        {
            recomApvList = _earlyOutRecomApvDataService.GetRecomApvListServiceData(year, toBrowse, recomOrApv, UserID);
            return ValidationResult.Success;
        }

        public ValidationResult GetRecomApvTableService(string year, string comp_code, string toBrowse, string recomOrApv, string UserID, out List<RecomApvData> recomApvTableDataList)
        {
            recomApvTableDataList = _earlyOutRecomApvDataService.GetRecomApvTableServiceData(year, comp_code, toBrowse, recomOrApv, UserID);
            return ValidationResult.Success;
        }

        public Response_data SubmitRecomApvService(List<RecomApvData> infoList)
        {
            return _earlyOutRecomApvDataService.SubmitRecomApvServiceData(infoList);
        }
    }
}
