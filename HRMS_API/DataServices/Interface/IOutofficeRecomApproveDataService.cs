using HRMS_API.Models.OutOfficeRecomApv;
using static HRMS_API.DataServices.DBHelper;

namespace HRMS_API.DataServices.Interface
{
    public interface IOutofficeRecomApproveDataService
    {
        List<RecomApv> GetRecomApvListServiceData(string year, string toBrowse, string recomOrApv, string UserID);
        List<RecomApvData> GetRecomApvTableServiceData(string year, string comp_code, string toBrowse, string recomOrApv, string UserID);
        Response_data SubmitRecomApvServiceData(List<RecomApvData> infoList);
    }
}
