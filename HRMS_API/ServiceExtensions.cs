using HRMS_API.DataServices.Class;
using HRMS_API.DataServices.Interface;
using HRMS_API.Services.Class;
using HRMS_API.Services.Interface;

namespace HRMS_API
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ITestService, TestService>();
            services.AddScoped<ITestDataService, TestDataService>();

            services.AddScoped<IHomeService, HomeService>();
            services.AddScoped<IHomeDataService, HomeDataService>();

            services.AddScoped<ILeaveApplicationService, LeaveApplicationService>();
            services.AddScoped<ILeaveApplicationDataService, LeaveApplicationDataService>();

            services.AddScoped<ILeaveRecomApvService, LeaveRecomApvService>();
            services.AddScoped<ILeaveRecomApvDataService, LeaveRecomApvDataService>();

            services.AddScoped<IChangePassService, ChangePassService>();
            services.AddScoped<IChangePassDataService, ChangePassDataService>();

            services.AddScoped<IOutofficeApplicationService, OutofficeApplicationService>();
            services.AddScoped<IOutofficeApplicationDataService, OutofficeApplicationDataService>();

            services.AddScoped<IOutofficeRecomApproveService, OutofficeRecomApproveService>();
            services.AddScoped<IOutofficeRecomApproveDataService, OutofficeRecomApproveDataService>();

            services.AddScoped<ILateComingApplicationService, LateComingApplicationService>();
            services.AddScoped<ILateComingApplicationDataService, LateComingApplicationDataService>();

            services.AddScoped<ILateComingRecomApvService, LateComingRecomApvService>();
            services.AddScoped<ILateComingRecomApvDataService, LateComingRecomApvDataService>();

            services.AddScoped<IEarlyOutApplicationService, EarlyOutApplicationService>();
            services.AddScoped<IEarlyOutApplicationDataService, EarlyOutApplicationDataService>();

            services.AddScoped<IEarlyOutRecomApvService, EarlyOutRecomApvService>();
            services.AddScoped<IEarlyOutRecomApvDataService, EarlyOutRecomApvDataService>();

            services.AddScoped<IReportsService, ReportsService>();
            services.AddScoped<IReportsDataService, ReportsDataService>();

            return services;
        }
    }
}
