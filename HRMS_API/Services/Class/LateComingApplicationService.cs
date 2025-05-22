using HRMS_API.DataServices.Class;
using HRMS_API.DataServices.Interface;
using HRMS_API.Models.LateComingApplication;
using HRMS_API.Services.Interface;
using System.ComponentModel.DataAnnotations;
using static HRMS_API.DataServices.DBHelper;

namespace HRMS_API.Services.Class
{
    public class LateComingApplicationService : ILateComingApplicationService
    {
        private readonly ILateComingApplicationDataService _lateComingApplicationDataService;

        public LateComingApplicationService(ILateComingApplicationDataService lateComingApplicationDataService)
        {
            _lateComingApplicationDataService = lateComingApplicationDataService;
        }


        public ValidationResult GetLateCauseService(string UserID, out List<LateCause> lateCauseList)
        {
            lateCauseList = _lateComingApplicationDataService.GetLateCauseServiceData(UserID);
            return ValidationResult.Success;
        }

        public ValidationResult GetPndLateApplicationListService(string UserID, string Year, out List<PndLateApplication> pndLateApplicationList)
        {
            pndLateApplicationList = _lateComingApplicationDataService.GetPndLateApplicationListServiceData(UserID, Year);
            return ValidationResult.Success;
        }

        public ValidationResult GetPreviousHistoryService(string UserID, string Year, out List<LateComingHistory> lateComingHistory)
        {
            lateComingHistory = _lateComingApplicationDataService.GetPreviousHistoryServiceData(UserID, Year);
            return ValidationResult.Success;
        }

        public Response_data SubmitApplicationService(SubmitData submitData)
        {
            return _lateComingApplicationDataService.SubmitApplicationServiceData(submitData);
        }
    }
}
