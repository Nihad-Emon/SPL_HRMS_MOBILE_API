namespace HRMS_API.Models.OutOfficeRecomApv
{
    public class RecomApv
    {
        public string? Comp_name { get; set; }
        public string? Comp_code { get; set; }
        public string? Pend_for_recom { get; set; }
        public string? Pnd_for_aprv { get; set; }
        public string? Pending_title { get; set; }
        public string? Outoff_aprv_allow { get; set; }
        public string? Outoff_recom_allow { get; set; }
    }

    public class RecomApvData
    {
        public string? OUTOFF_CODE { get; set; }
        public string? EMP_CODE { get; set; }
        public string? EMP_ID { get; set; }
        public string? EMP_NAME { get; set; }
        public string? DEPT_NAME { get; set; }
        public string? EMP_DESIG_NAME { get; set; }
        public string? OFD_TYPE_NAME { get; set; }
        public string? OFD_START_DATE { get; set; }
        public string? OFD_END_DATE { get; set; }
        public string? OFD_DAYS { get; set; }
        public string? OUTOFF_REASON { get; set; }
        public string? NEXT_SUPV_EMP_CODE { get; set; }
        public string? RCMD_EMP_NAME { get; set; }
        public string? APRV_EMP_NAME { get; set; }
        public string? OFD_APLN_STATUS { get; set; }
        public string? NOTE_FROM_LAST_LAYER { get; set; }
        public string? APPLICATION_DATE { get; set; }
        public string? OFD_START_TIME { get; set; }
        public string? OFD_END_TIME { get; set; }
        public string? DESTINATION { get; set; }
        public string? PndOrApv { get; set; }
        public string? comp_code { get; set; }
        public string? note { get; set; }
        public string? year { get; set; }
        public string? recomOrApv { get; set; }
        public string? UserID { get; set; }
    }  
    
}
