using System.Buffers.Text;

namespace HRMS_API.Models
{
    public class LoginRequest
    {
        public string? user { get; set; }
        public string? pass { get; set; }
    }

    public class EmployeeInfo
    {
        public string? Emp_name { get; set; }
        public string? Emp_id { get; set; }
        public string? Designation { get; set; }
        public string? picture { get; set; }
        public string? Recom_Power { get; set; }
        public string? Apv_Power { get; set; }
        public string? User_id { get; set; }
        public string? Company { get; set; }
        public string? User_name{ get; set; }
        public string? User_pass { get; set; }
        public string? token { get; set; }
        public string? Leav_apln_status { get; set; }
        public string? Leav_rcmd_stus { get; set; }
        public string? recom_emp_code { get; set; }
        public string? recom_emp_name { get; set; }
        public string? apv_emp_code { get; set; }
        public string? apv_emp_name { get; set; }
    }

    public class EmpInfoDetails
    {
        public string? statusFlag { get; set; }
        public string? recom_emp_code { get; set; }
        public string? recom_emp_name { get; set; }
        public string? apv_emp_code { get; set; }
        public string? apv_emp_name { get; set; }

    }
}
