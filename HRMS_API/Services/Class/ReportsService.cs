using HRMS_API.DataServices.Class;
using HRMS_API.DataServices.Interface;
using HRMS_API.Models.Reports;
using HRMS_API.Services.Interface;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace HRMS_API.Services.Class
{
    public class ReportsService : IReportsService
    {
        private readonly IReportsDataService _reportsDataService;
        private string _errorNumber = string.Empty;

        public ReportsService(IReportsDataService reportsDataService)
        {
            _reportsDataService = reportsDataService;
        }

        public ValidationResult GetJobCardService(string UserID, string FromDate, string ToDate, out List<JobCard> jobcard)
        {
            jobcard = _reportsDataService.GetJobCardServiceData(UserID, FromDate, ToDate);
            return ValidationResult.Success;
        }
    }
}
