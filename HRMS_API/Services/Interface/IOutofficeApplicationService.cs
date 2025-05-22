using HRMS_API.Models.Outoffice;
using System.ComponentModel.DataAnnotations;
using static HRMS_API.DataServices.DBHelper;

namespace HRMS_API.Services.Interface
{
    public interface IOutofficeApplicationService
    {
        Response_data SubmitApplicationService(SubmitData submitData);
        ValidationResult GetPreviousHistoryService(string UserID, string Year, out List<OutofficeHistory> outofficeHistories);
        ValidationResult GetPndAplnListService(string UserID, string Year, out List<PndLeaveApplication> pndLeaveApplicationList);
    }
}
