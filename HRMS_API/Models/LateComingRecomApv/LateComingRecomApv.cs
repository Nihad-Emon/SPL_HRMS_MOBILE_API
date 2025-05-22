namespace HRMS_API.Models.LateComingRecomApv
{
    public class RecomApv
    {
        public string? Comp_name { get; set; }
        public string? Comp_code { get; set; }
        public string? Pend_for_recom { get; set; }
        public string? Pnd_for_aprv { get; set; }
        public string? Pending_title { get; set; }
        public string? Late_aprv_allow { get; set; }
        public string? Late_recom_allow { get; set; }
    }


    public class RecomApvData
    {
        public string? ATD_LT_CODE { get; set; }
        public string? EMP_CODE { get; set; }
        public string? EMP_ID { get; set; }
        public string? EMP_NAME { get; set; }
        public string? DEPT_NAME { get; set; }
        public string? EMP_DESIG_NAME { get; set; }
        public string? LT_APLN_DATE { get; set; }
        public string? LT_CAUSE_NAME { get; set; }
        public string? LT_DATE { get; set; }
        public string? EXPECTED_IN_TIME { get; set; }
        public string? EXPECTED_LT_IN_MIN { get; set; }
        public string? VISIT_PLACE { get; set; }
        public string? RCMD_EMP_NAME { get; set; }
        public string? APRV_EMP_NAME { get; set; }
        public string? LT_RCMD_STUS { get; set; }
        public string? LT_APLN_STATUS { get; set; }
        public string? NOTE_FROM_LAST_LAYER { get; set; }
        public string? PndOrApv { get; set; }
        public string? comp_code { get; set; }
        public string? note { get; set; }
        public string? year { get; set; }
        public string? recomOrApv { get; set; }
        public string? UserID { get; set; }
    }
}
