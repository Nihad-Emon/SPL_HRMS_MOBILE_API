namespace HRMS_API.Models.RecomApprove
{
    public class RecomApv
    {
        public string? Comp_name { get; set; }
        public string? Comp_code { get; set; }
        public string? Pend_for_recom { get; set; }
        public string? Pending_title { get; set; }
        public string? Leav_aprv_allow { get; set; }
        public string? Leav_recom_allow { get; set; }
        public string? Pnd_for_aprv { get; set; }
    }

    public class RecomApvData
    {
        public int Leav_apln_slno { get; set; }
        public string? Emp_id { get; set; }
        public string? Emp_name { get; set; }
        public string? Emp_code { get; set; }
        public string? Leav_apln_code { get; set; }
        public string? Leav_apln_date { get; set; }
        public string? Dept_name { get; set; }
        public string? Emp_desig_name { get; set; }
        public string? Leav_type_name { get; set; }
        public string? Leav_type_code { get; set; }
        public string? Leave_catg_code { get; set; }
        public string? Leav_from_date { get; set; }
        public string? Leav_to_date { get; set; }
        public string? Leav_takn_days { get; set; }
        public string? Leav_takn_caus { get; set; }
        public string? Leav_days_stay { get; set; }
        public string? Rcmd_emp_name { get; set; }
        public string? Aprv_emp_name { get; set; }
        public string? Leav_apln_status { get; set; }
        public string? On_leav_phn_no { get; set; }
        public string? Blnc_leav_days { get; set; }
        public string? Leav_catg_code { get; set; }
        public string? PndOrApv { get; set; }
        public string? Note_from_last_layer { get; set; }
        public string? comp_code { get; set; }
        public string? note { get; set; }
        public string? year { get; set; }
        public string? recomOrApv { get; set; }
        public string? UserID { get; set; }
    }

    public class ChangeLeave
    {
        public string? leav_apln_emp_code { get; set; }
        public string? leave_type_code { get; set; }
        public string? leave_apln_code { get; set; }
        public string? from_date { get; set; }
        public string? to_date { get; set; }
        public string? taken_days { get; set; }
        public string? comp_code { get; set; }
        public string? note { get; set; }
        public string? UserID { get; set; }
    }
}
