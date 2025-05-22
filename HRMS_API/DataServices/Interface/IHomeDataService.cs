using HRMS_API.Models.Home;

namespace HRMS_API.DataServices.Interface
{
    public interface IHomeDataService
    {
        List<LeaveYear> GetLeavelYearServiceData(string UserID);
        List<Balance> GetBalanceServiceData(string UserID, string Year);
        List<Total> GetLeaveCountServiceData(string Year, string ToBrowse, string UserID);
        int GetPendingAplnCountServiceData(string UserID, string Year);
        List<PndLeaveApplication> GetPndLeaveApplicationListServiceData(string UserID, string Year);
        List<LeaveHistory> GetLeaveHistoryServiceData(string UserID, string Year);
        List<LeaveHistory> GetLeaveHistoryServiceForOthersData(string UserID, string Year, string comp_code);
        List<Absent> GetAbsentListServiceData(string UserID, string Year);
        List<IpPhone> GetIpPhoneListServiceData(string UserID);
        List<Holiday> GetHolidayListServiceData(string UserID);
    }
}
