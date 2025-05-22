using HRMS_API.DataServices.Interface;
using HRMS_API.Models.Home;
using HRMS_API.Models.LeaveApplication;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using static HRMS_API.DataServices.DBHelper;

namespace HRMS_API.DataServices.Class
{
    public class LeaveApplicationDataService : ILeaveApplicationDataService
    {
        readonly IConfiguration _configuration;
        private readonly OracleDBConnection OracleDBConnection;
        private readonly DBHelper dbHelper;

        public LeaveApplicationDataService(IConfiguration configuration)
        {
            _configuration = configuration;
            OracleDBConnection = new OracleDBConnection(configuration);
            dbHelper = new DBHelper(configuration);
        }

        

        public List<LeaveType> GetLeaveTypeServiceData(string UserID, string Year)
        {
            OracleConnection Oracleconnection = new OracleConnection(OracleDBConnection.ConnectionStringRead());

            var leaveTypeList = new List<LeaveType>();

            try
            {
                OracleCommand objCmd = new OracleCommand("pkg01_hrms_lap01.f_leav_type_display ", Oracleconnection);
                DataTable dt = new DataTable();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("p_emp_code", OracleDbType.Varchar2).Value = UserID;
                objCmd.Parameters["p_emp_code"].Direction = ParameterDirection.Input;
                objCmd.Parameters.Add("p_leav_year", OracleDbType.Varchar2).Value = Year;
                objCmd.Parameters["p_leav_year"].Direction = ParameterDirection.Input;



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
                    leaveTypeList.Add(new LeaveType {
                        Leave_type_name = row["leav_type_name"].ToString(),
                        Leave_type_Short_name = row["leav_type_srtnm"].ToString(),
                        Leave_type_code = row["leav_type_code"].ToString(),
                        Leave_catg_code = row["leav_catg_code"].ToString()
                    });
                }

                Oracleconnection.Close();
                Oracleconnection.Dispose();
                return leaveTypeList;
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



        public List<CalculatedDays> GetCalDaysService(string leav_catg_code, string start_date, string end_date, string emp_code, string leav_type_code, string Year)
        {
            OracleConnection Oracleconnection = new OracleConnection(OracleDBConnection.ConnectionStringRead());

            var DaysData = new List<CalculatedDays>();

            try
            {
                OracleCommand objCmd = new OracleCommand("pkg01_hrms_lap01.f_leav_cal_days_display ", Oracleconnection);
                DataTable dt = new DataTable();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("p_leav_catg_code", OracleDbType.Varchar2).Value = leav_catg_code;
                objCmd.Parameters["p_leav_catg_code"].Direction = ParameterDirection.Input;
                objCmd.Parameters.Add("p_start_date", OracleDbType.Varchar2).Value = start_date;
                objCmd.Parameters["p_start_date"].Direction = ParameterDirection.Input;
                objCmd.Parameters.Add("p_end_date", OracleDbType.Varchar2).Value = end_date;
                objCmd.Parameters["p_end_date"].Direction = ParameterDirection.Input;
                objCmd.Parameters.Add("p_emp_code", OracleDbType.Varchar2).Value = emp_code;
                objCmd.Parameters["p_emp_code"].Direction = ParameterDirection.Input;
                objCmd.Parameters.Add("p_leav_type_code", OracleDbType.Varchar2).Value = leav_type_code;
                objCmd.Parameters["p_leav_type_code"].Direction = ParameterDirection.Input;
                objCmd.Parameters.Add("p_year", OracleDbType.Varchar2).Value = Year;
                objCmd.Parameters["p_year"].Direction = ParameterDirection.Input;



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
                    DaysData.Add(new CalculatedDays
                    {
                        Days = row["cal_days"].ToString()
                    });
                }
                Oracleconnection.Close();
                Oracleconnection.Dispose();
                return DaysData;
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

        public Response_data SubmitApplicationServiceData(List<SubmitData> submitData)
        {
            string status = string.Empty, message = string.Empty /*statusandmessage = string.Empty*/;
            var statusandmessage = new Response_data();

            using (OracleConnection connection = new OracleConnection(OracleDBConnection.ConnectionStringRead()))
            {
                // create a new OracleCommand object
                using (OracleCommand command = new OracleCommand("pkg01_hrms_lap01.p_leav_apln_save", connection))
                {
                    try
                    {
                        // set the command type to stored procedure
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        // add any parameters the stored procedure requires

                        command.Parameters.Add("p_emp_code", OracleDbType.Varchar2).Value = submitData[0].UserID;
                        command.Parameters.Add("p_leav_type_code", OracleDbType.Varchar2).Value = submitData[0].leav_type_code;
                        command.Parameters.Add("p_leav_from_date", OracleDbType.Varchar2).Value = submitData[0].leav_from_date;
                        command.Parameters.Add("p_leav_to_date", OracleDbType.Varchar2).Value = submitData[0].leav_to_date;
                        command.Parameters.Add("p_leav_takn_days", OracleDbType.Varchar2).Value = submitData[0].leav_takn_days;
                        command.Parameters.Add("p_leav_takn_caus", OracleDbType.Varchar2).Value = submitData[0].leav_takn_caus;
                        command.Parameters.Add("p_leav_days_stay", OracleDbType.Varchar2).Value = submitData[0].leav_days_stay;
                        command.Parameters.Add("p_on_leav_phn_no", OracleDbType.Varchar2).Value = submitData[0].leav_phn_no;
                        command.Parameters.Add("p_next_supv_emp_code", OracleDbType.Varchar2).Value = submitData[0].next_supv_emcode;
                        command.Parameters.Add("p_leav_apln_status", OracleDbType.Varchar2).Value = submitData[0].leav_apln_status;
                        command.Parameters.Add("p_leav_rcmd_stus", OracleDbType.Varchar2).Value = submitData[0].leav_rcmd_stus;
                        command.Parameters.Add("p_year", OracleDbType.Varchar2).Value = submitData[0].year;
                        command.Parameters.Add("p_dml_status", OracleDbType.Varchar2).Value = submitData[0].dml_status;
                        command.Parameters.Add("p_leav_catg_code", OracleDbType.Varchar2).Value = submitData[0].leav_catg_code;

                        command.Parameters.Add("p_entered_by", OracleDbType.Varchar2).Value = submitData[0].UserID;
                        command.Parameters.Add("p_work_station", OracleDbType.Varchar2).Value = Environment.MachineName;

                        command.Parameters.Add("p_out", OracleDbType.Int32, 100).Direction = ParameterDirection.Output;
                        command.BindByName = true;

                        connection.Open();
                        command.ExecuteNonQuery();

                        string returnValue = Convert.ToString(command.Parameters["p_out"].Value);

                        statusandmessage = dbHelper.GetStatusAndMessage("HRMF_LAP01", returnValue.ToString());
                        connection.Close();
                        connection.Dispose();
                    }
                    catch (Exception ex)
                    {
                        connection.Close();
                        connection.Dispose();
                        throw;
                    }
                    finally
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
            }
            return statusandmessage;
        }
    }
}
