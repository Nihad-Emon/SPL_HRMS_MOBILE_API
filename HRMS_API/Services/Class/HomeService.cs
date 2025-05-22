using HRMS_API.DataServices.Interface;
using HRMS_API.Models.Home;
using HRMS_API.Services.Interface;
using System.ComponentModel.DataAnnotations;

namespace HRMS_API.Services.Class
{
    public class HomeService : IHomeService
    {
        private readonly IHomeDataService _homeDataService;

        public HomeService(IHomeDataService homeDataService)
        {
            _homeDataService = homeDataService;
        }        

        public ValidationResult GetLeavelYearService(string UserID, out List<LeaveYear> LeaveYearList)
        {
            LeaveYearList = _homeDataService.GetLeavelYearServiceData(UserID);
            return ValidationResult.Success;
        }

        public ValidationResult GetBalanceService(string UserID, string Year, out List<Balance> Balance)
        {
            Balance = _homeDataService.GetBalanceServiceData(UserID, Year);
            return ValidationResult.Success;
        }

        public ValidationResult GetLeaveCountService(string Year, string ToBrowse, string UserID, out List<Total> Total)
        {
            Total = _homeDataService.GetLeaveCountServiceData(Year, ToBrowse, UserID);
            return ValidationResult.Success;
        }

        public ValidationResult GetPendingAplnCountService(string UserID, string Year, out int pending)
        {
            pending = _homeDataService.GetPendingAplnCountServiceData(UserID, Year);
            return ValidationResult.Success;
        }
        public ValidationResult GetPndLeaveApplicationListService(string UserID, string Year, out List<PndLeaveApplication> pndLeaveApplicationList)
        {
            pndLeaveApplicationList = _homeDataService.GetPndLeaveApplicationListServiceData(UserID, Year);
            return ValidationResult.Success;
        }

        public ValidationResult GetLeaveHistoryService(string UserID, string Year, out List<LeaveHistory> leaveHistoryList)
        {
            leaveHistoryList = _homeDataService.GetLeaveHistoryServiceData(UserID, Year);
            return ValidationResult.Success;
        }
        
        public ValidationResult GetLeaveHistoryServiceForOthers(string UserID, string Year, string comp_code, out List<LeaveHistory> leaveHistoryList)
        {
            leaveHistoryList = _homeDataService.GetLeaveHistoryServiceForOthersData(UserID, Year, comp_code);
            return ValidationResult.Success;
        }

        public ValidationResult GetAbsentListService(string UserID, string Year, out List<Absent> absentList)
        {
            absentList = _homeDataService.GetAbsentListServiceData(UserID, Year);
            return ValidationResult.Success;
        }

        public ValidationResult GetIpPhoneListService(string UserID, out List<IpPhone> ipPhoneList)
        {
            ipPhoneList = _homeDataService.GetIpPhoneListServiceData(UserID);
            return ValidationResult.Success;
        }

        public ValidationResult GetHolidayListService(string UserID, out List<Holiday> holidayList)
        {
            holidayList = _homeDataService.GetHolidayListServiceData(UserID);
            return ValidationResult.Success;
        }
    }
}
