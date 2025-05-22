using HRMS_API.DataServices.Interface;
using HRMS_API.Services.Interface;

namespace HRMS_API.Services.Class
{
    public class TestService : ITestService
    {
        private readonly ITestDataService _testDataService;

        public TestService(ITestDataService testDataService)
        {
            _testDataService = testDataService;
        }
        public string GetTestService()
        {
            return _testDataService.GetTestServiceData();
        }
    }
}
