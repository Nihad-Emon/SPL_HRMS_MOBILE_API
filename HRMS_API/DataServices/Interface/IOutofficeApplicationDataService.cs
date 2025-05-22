using HRMS_API.Models.Outoffice;
using static HRMS_API.DataServices.DBHelper;

namespace HRMS_API.DataServices.Interface
{
    public interface IOutofficeApplicationDataService
    {
        Response_data SubmitApplicationServiceData(SubmitData submitData);
        List<OutofficeHistory> GetPreviousHistoryServiceData(string UserID, string Year);
        List<PndLeaveApplication> GetPndAplnListServiceData(string UserID, string Year);
    }
}
