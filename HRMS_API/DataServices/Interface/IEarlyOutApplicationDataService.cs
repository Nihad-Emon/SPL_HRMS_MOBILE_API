using HRMS_API.Models.EarlyOutApplication;
using static HRMS_API.DataServices.DBHelper;

namespace HRMS_API.DataServices.Interface
{
    public interface IEarlyOutApplicationDataService
    {
        List<EarlyOutCause> GetEarlyOutCauseServiceData(string UserID);
        Response_data SubmitApplicationServiceData(SubmitData submitData);
        List<PndEarlyOutApplication> GetPndEarlyOutApplicationListServiceData(string UserID, string Year);
        List<EarlyOutHistory> GetPreviousHistoryServiceData(string UserID, string Year);
    }
}
