using HRMS_API.Models;
using static HRMS_API.DataServices.DBHelper;

namespace HRMS_API.Services.Interface
{
    public interface IChangePassService
    {
        Response_data ChangePassService(SecUser objUser, string oldPassword, string action);
    }
}
