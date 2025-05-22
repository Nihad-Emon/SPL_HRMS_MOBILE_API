using HRMS_API.DataServices.Interface;

namespace HRMS_API.DataServices.Class
{
    public class TestDataService : ITestDataService
    {
        public string GetTestServiceData()
        {
            var leaveYearList = "Hello!";
            return leaveYearList;
        }
    }
}
