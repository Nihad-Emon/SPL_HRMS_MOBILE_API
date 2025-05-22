using HRMS_API.Models.Reports;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace HRMS_API.Services.Interface
{
    public interface IReportsService
    {
        ValidationResult GetJobCardService(string UserID, string FromDate, string ToDate, out List<JobCard> jobcard);
    }
}
