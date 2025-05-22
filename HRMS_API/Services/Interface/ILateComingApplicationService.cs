using HRMS_API.Models.LateComingApplication;
using System.ComponentModel.DataAnnotations;
using static HRMS_API.DataServices.DBHelper;

namespace HRMS_API.Services.Interface
{
    public interface ILateComingApplicationService
    {
        ValidationResult GetLateCauseService(string UserID, out List<LateCause> LateCauseList);
        Response_data SubmitApplicationService(SubmitData submitData);
        ValidationResult GetPndLateApplicationListService(string UserID, string Year, out List<PndLateApplication> pndLateApplicationList);
        ValidationResult GetPreviousHistoryService(string UserID, string Year, out List<LateComingHistory> outofficeHistories);
    }
}
