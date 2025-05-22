namespace HRMS_API.Models.LeaveApplication
{
    public class LeaveType
    {
        public string? Leave_type_name { get; set; }
        public string? Leave_type_Short_name { get; set; }
        public string? Leave_type_code { get; set; }
        public string? Leave_catg_code { get; set; }
    }

    public class CalculatedDays { 
        public string? Days { get; set; }    
    }

    public class CalDaysInfo
    {
        public string? UserID { get; set; }
        public string? leav_catg_code { get; set; }
        public string? start_date { get; set; }
        public string? end_date { get; set; }
        public string? leav_type_code { get; set; }
        public string? Year { get; set; }
    }

    public class SubmitData
    {
        public string? leav_type_code { get; set; }
        public string? leav_from_date { get; set; }
        public string? leav_to_date { get; set; }
        public string? leav_takn_days { get; set; }
        public string? leav_takn_caus { get; set; }
        public string? leav_days_stay { get; set; }
        public string? leav_phn_no { get; set; }
        public string? next_supv_emcode { get; set; }
        public string? leav_apln_status { get; set; }
        public string? leav_rcmd_stus { get; set; }
        public string? year { get; set; }
        public string? dml_status { get; set; }
        public string? leav_catg_code { get; set; }
        public string? UserID { get; set; }
    }

}
