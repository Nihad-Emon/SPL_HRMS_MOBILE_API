using HRMS_API.DataServices.Interface;
using HRMS_API.Models;
using HRMS_API.Services.Interface;
using static HRMS_API.DataServices.DBHelper;

namespace HRMS_API.Services.Class
{
    public class ChangePassService : IChangePassService
    {
        private readonly IChangePassDataService _changePassDataService;

        public ChangePassService(IChangePassDataService changePassDataService) { 
            _changePassDataService = changePassDataService;
        }
        Response_data IChangePassService.ChangePassService(SecUser objUser, string oldPassword, string action)
        {
            return _changePassDataService.ChangePassServiceData(objUser, oldPassword, action);
        }
    }
}
