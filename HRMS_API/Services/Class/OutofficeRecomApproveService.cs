using HRMS_API.DataServices;
using HRMS_API.DataServices.Class;
using HRMS_API.DataServices.Interface;
using HRMS_API.Models.OutOfficeRecomApv;
using HRMS_API.Services.Interface;
using System.ComponentModel.DataAnnotations;

namespace HRMS_API.Services.Class
{
    public class OutofficeRecomApproveService : IOutofficeRecomApproveService
    {
        private readonly IOutofficeRecomApproveDataService _outOfficeRecomApproveDataService;

        public OutofficeRecomApproveService(IOutofficeRecomApproveDataService outofficeRecomApproveDataService)
        {
            _outOfficeRecomApproveDataService = outofficeRecomApproveDataService;
        }


        public ValidationResult GetRecomApvListService(string year, string toBrowse, string recomOrApv, string UserID, out List<RecomApv> recomApvList)
        {
            recomApvList = _outOfficeRecomApproveDataService.GetRecomApvListServiceData(year, toBrowse, recomOrApv, UserID);
            return ValidationResult.Success;
        }

        public ValidationResult GetRecomApvTableService(string year, string comp_code, string toBrowse, string recomOrApv, string UserID, out List<RecomApvData> recomApvTableDataList)
        {
            recomApvTableDataList = _outOfficeRecomApproveDataService.GetRecomApvTableServiceData(year, comp_code, toBrowse, recomOrApv, UserID);
            return ValidationResult.Success;
        }

        public DBHelper.Response_data SubmitRecomApvService(List<RecomApvData> infoList)
        {
            return _outOfficeRecomApproveDataService.SubmitRecomApvServiceData(infoList);
        }
    }
}
