using HRMS_API.Models.OutOfficeRecomApv;
using System.ComponentModel.DataAnnotations;
using static HRMS_API.DataServices.DBHelper;

namespace HRMS_API.Services.Interface
{
    public interface IOutofficeRecomApproveService
    {
        ValidationResult GetRecomApvListService(string year, string toBrowse, string recomOrApv, string UserID, out List<RecomApv> recomApvList);
        ValidationResult GetRecomApvTableService(string year, string comp_code, string toBrowse, string recomOrApv, string UserID, out List<RecomApvData> recomApvTableDataList);
        Response_data SubmitRecomApvService(List<RecomApvData> infoList);
    }
}
