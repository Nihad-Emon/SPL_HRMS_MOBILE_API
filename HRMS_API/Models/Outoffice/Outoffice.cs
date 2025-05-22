namespace HRMS_API.Models.Outoffice
{
    public class SubmitData
    {
        public string? emcode { get; set; }
        public string? leav_from_date { get; set; }
        public string? leav_to_date { get; set; }
        public string? leav_from_time { get; set; }
        public string? leav_to_time { get; set; }
        public string? leav_takn_days { get; set; }
        public string? leav_takn_caus { get; set; }
        public string? duty { get; set; }
        public string? Remarks { get; set; }
        public string? next_supv_emcode { get; set; }
        public string? leav_apln_status { get; set; }
        public string? year { get; set; }
        public string? dml_status { get; set; }
        public string? UserID { get; set; }
    }

    public class OutofficeHistory
    {
        public int SL { get; set; }
        public string? Leave_days { get; set; }
        public string? Leave_detail { get; set; }
        public string? colorStatus { get; set; }
        public string? From_date { get; set; }
        public string? To_date { get; set; }
        public string? Start_Date { get; set; }
        public string? End_Date { get; set; }
        public string? Start_Time { get; set; }
        public string? End_Time { get; set; }
    }

    public class PndLeaveApplication
    {
        public int SL { get; set; }
        public string? Type { get; set; }
        public string? Application_date { get; set; }
        public string? From_date { get; set; }
        public string? To_date { get; set; }
        public string? Days { get; set; }
        public string? Leav_takn_caus { get; set; }
        public string? Leav_days_stay { get; set; }
        public string? Rcmd_emp_name { get; set; }
        public string? Aprv_emp_name { get; set; }
        public string? Duty_Place { get; set; }
        public string? Status { get; set; }
        public string? ColorStatus { get; set; }
        public string? Cause { get; set; }
        public string? Sttime { get; set; }
        public string? Endtime { get; set; }

    }
}
