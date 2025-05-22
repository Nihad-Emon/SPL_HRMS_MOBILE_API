using HRMS_API.DataServices;
using HRMS_API.DataServices.Interface;
using HRMS_API.Models.Outoffice;
using HRMS_API.Services.Interface;
using System.ComponentModel.DataAnnotations;
using static HRMS_API.DataServices.DBHelper;

namespace HRMS_API.Services.Class
{
    public class OutofficeApplicationService : IOutofficeApplicationService
    {
        private readonly IOutofficeApplicationDataService _outOfficeApplicationDataService;
        public OutofficeApplicationService(IOutofficeApplicationDataService outofficeApplicationDataService)
        {
            _outOfficeApplicationDataService = outofficeApplicationDataService;
        }

        public ValidationResult GetPndAplnListService(string UserID, string Year, out List<PndLeaveApplication> pndLeaveApplicationList)
        {
            pndLeaveApplicationList = _outOfficeApplicationDataService.GetPndAplnListServiceData(UserID, Year);
            return ValidationResult.Success;
        }

        public ValidationResult GetPreviousHistoryService(string UserID, string Year, out List<OutofficeHistory> outofficeHistories)
        {
            outofficeHistories = _outOfficeApplicationDataService.GetPreviousHistoryServiceData(UserID, Year);
            return ValidationResult.Success;
        }

        public Response_data SubmitApplicationService(SubmitData submitData)
        {
            return _outOfficeApplicationDataService.SubmitApplicationServiceData(submitData);
        }
    }
}
