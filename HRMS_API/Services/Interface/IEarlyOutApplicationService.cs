using HRMS_API.Models.EarlyOutApplication;
using System.ComponentModel.DataAnnotations;
using static HRMS_API.DataServices.DBHelper;

namespace HRMS_API.Services.Interface
{
    public interface IEarlyOutApplicationService
    {
        ValidationResult GetEarlyOutCauseService(string UserID, out List<EarlyOutCause> earltOutCauseList);
        Response_data SubmitApplicationService(SubmitData submitData);
        ValidationResult GetPndEarlyOutApplicationListService(string UserID, string Year, out List<PndEarlyOutApplication> pndEarlyOutApplicationList);
        ValidationResult GetPreviousHistoryService(string UserID, string Year, out List<EarlyOutHistory> earlyOutHistory);
    }
}
