using HRMS_API.Models;

namespace HRMS_API.Controllers.Security
{
    public class SharedServices
    {
        // Static property to hold the shared data
        public static string SharedData { get; set; }
        public static string SharedData2 { get; set; }

        public SharedServices() { }

        // Method to set the shared data
        public void SetSharedData(EmployeeInfo data)
        {
            SharedData = data.User_id.ToString();
            SharedData2 = data.User_name.ToString();
        }

        // Method to get the shared data
        public string GetSharedData()
        {
            return SharedData;
        }
        public string GetSharedData2()
        {
            return SharedData2;
        }
    }
}
