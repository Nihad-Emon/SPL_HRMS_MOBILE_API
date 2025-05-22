namespace HRMS_API.Models
{
    public class SecUser
    {
        public string? Set_by { get; set; }
        public string? User_id { get; set; }
        public string? Mod_by { get; set; }
        public string? User_name { get; set; }
        public string? User_pass { get; set; }
    }

    public class ChangeInfo { 
        public string? password { get; set; }
        public string? oldPassword { get; set; }
        public string? User_id { get; set; }
        public string? User_name { get; set; }

    }

}
