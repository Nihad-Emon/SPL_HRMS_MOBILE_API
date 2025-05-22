using HRMS_API.Models.Reports;
using System.Data;

namespace HRMS_API.DataServices.Interface
{
    public interface IReportsDataService
    {
        List<JobCard> GetJobCardServiceData(string UserID, string FromDate, string ToDate);
    }
}
