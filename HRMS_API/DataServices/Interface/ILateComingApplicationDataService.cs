using HRMS_API.Models.LateComingApplication;
using static HRMS_API.DataServices.DBHelper;

namespace HRMS_API.DataServices.Interface
{
    public interface ILateComingApplicationDataService
    {
        List<LateCause> GetLateCauseServiceData(string UserID);
        Response_data SubmitApplicationServiceData(SubmitData submitData);
        List<PndLateApplication> GetPndLateApplicationListServiceData(string UserID, string Year);
        List<LateComingHistory> GetPreviousHistoryServiceData(string UserID, string Year);
    }
}
