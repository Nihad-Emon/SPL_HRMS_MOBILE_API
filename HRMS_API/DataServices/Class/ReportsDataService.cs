using HRMS_API.DataServices.Interface;
using HRMS_API.Models.Home;
using HRMS_API.Models.Reports;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace HRMS_API.DataServices.Class
{
    public class ReportsDataService : IReportsDataService
    {
        readonly IConfiguration _configuration;
        private readonly OracleDBConnection OracleDBConnection;

        public ReportsDataService(IConfiguration configuration)
        {
            _configuration = configuration;
            OracleDBConnection = new OracleDBConnection(configuration);
        }
        public List<JobCard> GetJobCardServiceData(string UserID, string FromDate, string ToDate)
        {
            OracleConnection Oracleconnection = new OracleConnection(OracleDBConnection.ConnectionStringRead());
            var jobData = new List<JobCard>();


            try
            {
                OracleCommand objCmd = new OracleCommand("pkg21_hrms_ind01_bk.f_emp_job_card_dis ", Oracleconnection);
                DataTable dt = new DataTable();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("p_emp_code", OracleDbType.Varchar2).Value = UserID;
                objCmd.Parameters.Add("p_from_date", OracleDbType.Varchar2).Value = FromDate;
                objCmd.Parameters.Add("p_to_date", OracleDbType.Varchar2).Value = ToDate;

                objCmd.Parameters.Add("return_value", OracleDbType.RefCursor).Direction = ParameterDirection.ReturnValue;
                objCmd.BindByName = true;

                Oracleconnection.Open();

                using (OracleDataReader odr = objCmd.ExecuteReader())
                {
                    if (odr.HasRows)
                    {
                        dt.Load(odr);
                    }
                }

                foreach (DataRow row in dt.Rows)
                {
                    jobData.Add(new JobCard
                    {
                        comp_name = row[":B5"].ToString(),
                        comp_address = row[":B4"].ToString(),
                        emp_id = row["emp_id"].ToString(),
                        emp_name = row["emp_name"].ToString(),
                        ename = row["ename"].ToString(),
                        emp_desig_name = row["emp_desig_name"].ToString(),
                        dept_name = row["dept_name"].ToString(),
                        sect_name = row["sect_name"].ToString(),
                        emp_status_name = row["emp_status_name"].ToString(),
                        joining_date = row["joining_date"].ToString(),
                        atdn_date = row["atdn_date"].ToString(),
                        atdn_day = row["atdn_day"].ToString(),
                        shift_no = row["shift_no"].ToString(),
                        actual_in_time = row["actual_in_time"].ToString(),
                        actual_out_time = row["actual_out_time"].ToString(),
                        calculated_ot = row["calculated_ot"].ToString(),
                        process_type = row["process_type"].ToString(),
                        late = row["late"].ToString(),
                        eo = row["eo"].ToString(),
                        work_duration = row["work_duration"].ToString(),
                        atdn_remarks = row["atdn_remarks"].ToString(),
                        atdn_status_name = row["atdn_status_name"].ToString(),
                        total_present = row["total_present"].ToString(),
                        actual_present = row["actual_present"].ToString(),
                        total_absent = row["total_absent"].ToString(),
                        total_weekend = row["total_weekend"].ToString(),
                        total_holiday = row["total_holiday"].ToString(),
                        total_leave = row["total_leave"].ToString(),
                        total_out_office_days = row["total_out_office_days"].ToString(),
                        total_tour_days = row["total_tour_days"].ToString(),
                        total_eo_days = row["total_eo_days"].ToString(),
                        total_aprv_eo = row["total_aprv_eo"].ToString(),
                        total_woaprv_eo = row["total_woaprv_eo"].ToString(),
                        total_eo_in_mnt = row["total_eo_in_mnt"].ToString(),
                        total_lt_days = row["total_lt_days"].ToString(),
                        total_aprv_lt = row["total_aprv_lt"].ToString(),
                        total_woaprv_lt = row["total_woaprv_lt"].ToString(),
                        total_lt_in_min = row["total_lt_in_min"].ToString(),
                        total_incomplete = row["total_incomplete"].ToString(),
                        total_ot = row["total_ot"].ToString(),
                        total_ot_days = row["total_ot_days"].ToString(),
                        total_working_hour = row["total_working_hour"].ToString()
                    });
                }

                Oracleconnection.Close();
                Oracleconnection.Dispose();
                return jobData;
            }
            catch (Exception)
            {
                Oracleconnection.Close();
                Oracleconnection.Dispose();
                throw;
            }
            finally
            {
                Oracleconnection.Close();
                Oracleconnection.Dispose();
            }
        }
    }
}
