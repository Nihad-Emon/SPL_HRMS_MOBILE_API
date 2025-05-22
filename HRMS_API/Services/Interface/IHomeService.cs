using HRMS_API.Models.Home;
using System.ComponentModel.DataAnnotations;

namespace HRMS_API.Services.Interface
{
    public interface IHomeService
    {
        ValidationResult GetLeavelYearService(string UserID, out List<LeaveYear> LeaveYearList);
        ValidationResult GetBalanceService(string UserID, string Year, out List<Balance> Balance);
        ValidationResult GetLeaveCountService(string Year, string ToBrowse, string UserID, out List<Total> Total);
        ValidationResult GetPendingAplnCountService(string UserID, string Year, out int pending);
        ValidationResult GetPndLeaveApplicationListService(string UserID, string Year, out List<PndLeaveApplication> pndLeaveApplicationList);
        ValidationResult GetLeaveHistoryService(string UserID, string Year, out List<LeaveHistory> leaveHistoryList);
        ValidationResult GetLeaveHistoryServiceForOthers(string UserID, string Year, string comp_code, out List<LeaveHistory> leaveHistoryList);
        ValidationResult GetAbsentListService(string UserID, string Year, out List<Absent> absentList);
        ValidationResult GetIpPhoneListService(string UserID, out List<IpPhone> ipPhoneList);
        ValidationResult GetHolidayListService(string UserID, out List<Holiday> holidayList);
    }
}
