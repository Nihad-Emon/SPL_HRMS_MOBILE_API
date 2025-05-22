using HRMS_API.Models;
using static HRMS_API.DataServices.DBHelper;

namespace HRMS_API.DataServices.Interface
{
    public interface IChangePassDataService
    {
        Response_data ChangePassServiceData(SecUser objUser, string oldPassword, string action);
    }
}
