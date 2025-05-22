using HRMS_API.DataServices.Class;
using HRMS_API.DataServices.Interface;
using HRMS_API.Models.EarlyOutApplication;
using HRMS_API.Services.Interface;
using System.ComponentModel.DataAnnotations;
using static HRMS_API.DataServices.DBHelper;

namespace HRMS_API.Services.Class
{
    public class EarlyOutApplicationService : IEarlyOutApplicationService
    {
        private readonly IEarlyOutApplicationDataService _earlyOutApplicationDataService;

        public EarlyOutApplicationService(IEarlyOutApplicationDataService earlyOutApplicationDataService)
        {
            _earlyOutApplicationDataService = earlyOutApplicationDataService;
        }

        public ValidationResult GetEarlyOutCauseService(string UserID, out List<EarlyOutCause> earltOutCauseList)
        {
            earltOutCauseList = _earlyOutApplicationDataService.GetEarlyOutCauseServiceData(UserID);
            return ValidationResult.Success;
        }

        public ValidationResult GetPndEarlyOutApplicationListService(string UserID, string Year, out List<PndEarlyOutApplication> pndEarlyOutApplicationList)
        {
            pndEarlyOutApplicationList = _earlyOutApplicationDataService.GetPndEarlyOutApplicationListServiceData(UserID, Year);
            return ValidationResult.Success;
        }

        public ValidationResult GetPreviousHistoryService(string UserID, string Year, out List<EarlyOutHistory> earlyOutHistory)
        {
            earlyOutHistory = _earlyOutApplicationDataService.GetPreviousHistoryServiceData(UserID, Year);
            return ValidationResult.Success;
        }

        public Response_data SubmitApplicationService(SubmitData submitData)
        {
            return _earlyOutApplicationDataService.SubmitApplicationServiceData(submitData);
        }
    }
}
