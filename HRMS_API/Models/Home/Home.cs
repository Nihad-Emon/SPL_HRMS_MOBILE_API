namespace HRMS_API.Models.Home
{
    public class LeaveYear
    {
        public string? Year { get; set; }
    }

    public class Balance
    {
        public string? Leave_type_name { get; set; }
        public string? Leave_type_Short_name { get; set; }
        public string? Leave_type_code { get; set; }
        public string? Leave_catg_code { get; set; }
        public string? Alotted_days { get; set; }
        public string? Taken_days { get; set; }
        public string? Balance_days { get; set; }
    }


    public class Total
    {
        public int pending { get; set; }

        public int approval { get; set; }

        public int total { get; set; }
    }

    public class PndAplnCount
    {
        public int pending { get; set; }
    }

    public class PndLeaveApplication {
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

    public class LeaveHistory
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


    public class Absent
    {
        public int SL { get; set; }
        public string? Month { get; set; }
        public string? Date { get; set; }
        public string? Day { get; set; }
    }


    public class IpPhone
    {
        public string? Emp_Name { get; set; }

        public string? IpNumber { get; set; }

        public string? Designation { get; set; }

        public string? Department { get; set; }

        public string? WorkStation { get; set; }
    }


    public class Holiday
    {
        public string? date { get; set; }

        public string? day { get; set; }

        public string? holiday { get; set; }
    }


}
