namespace HRMS_API.Models.EarlyOutApplication
{
    public class EarlyOutCause
    {
        public string? EarlyOut_cause_name { get; set; }
        public string? EarlyOut_cause_code { get; set; }
    }

    public class SubmitData
    {
        public string? EMP_CODE { get; set; }
        public string? EO_CAUSE_CODE { get; set; }
        public string? YEAR { get; set; }
        public string? EO_DATE { get; set; }
        public string? EXPECTED_EO_TIME { get; set; }
        public string? EXPECTED_RETURN_TIME { get; set; }
        public string? NO_RETURN_FLAG { get; set; }
        public string? VISIT_PLACE { get; set; }
        public string? EO_REMARKS { get; set; }
        public string? NEXT_SUPV_EMP_CODE { get; set; }
        public string? EO_APLN_STATUS { get; set; }
        public string? DML_STATUS { get; set; }
        public string? UserID { get; set; }

    }

    public class PndEarlyOutApplication
    {
        public string? EarlyOut_apln_date { get; set; }
        public string? EarlyOut_date { get; set; }
        public string? Expected_earlyOut_time { get; set; }
        public string? Expected_return_time { get; set; }
        public string? EarlyOut_apln_status { get; set; }
        public string? EarlyOut_cause_name { get; set; }
        public string? EarlyOut_apln_status_details { get; set; }
    }

    public class EarlyOutHistory
    {
        public string? EarlyOut_details { get; set; }
        public string? EarlyOut_date { get; set; }
        public string? EarlyOut_time { get; set; }
        public string? Return_time { get; set; }
    }
}
