namespace HRMS_API.Models.LateComingApplication
{
    public class SubmitData
    {
        public string? late_cause_code { get; set; }
        public string? late_date { get; set; }
        public string? expected_in_time { get; set; }
        public string? expected_late_in_min { get; set; }
        public string? remarks { get; set; }
        public string? next_supv_emcode { get; set; }
        public string? late_apln_status { get; set; }
        public string? year { get; set; }
        public string? dml_status { get; set; }
        public string? UserID { get; set; }
    }

    public class LateCause
    {
        public string? Late_cause_name { get; set; }
        public string? Late_cause_code { get; set; }
    }


    public class PndLateApplication
    {
        public string? application_date { get; set; }
        public string? late_date { get; set; }
        public string? expected_in_time { get; set; }
        public string? expected_late_in_time { get; set; }
        public string? late_apln_status { get; set; }
        public string? late_cause_name { get; set; }
        public string? late_apln_status_details { get; set; }
    }


    public class LateComingHistory
    {
        public string? late_detail { get; set; }
        public string? late_date { get; set; }
        public string? expected_in_time { get; set; }
        public string? expected_late_in_time { get; set; }
    }
}
