using HRMS_API.DataServices.Interface;
using HRMS_API.Models.LateComingApplication;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using static HRMS_API.DataServices.DBHelper;

namespace HRMS_API.DataServices.Class
{
    public class LateComingApplicationDataService : ILateComingApplicationDataService
    {
        readonly IConfiguration _configuration;
        private readonly OracleDBConnection OracleDBConnection;
        private readonly DBHelper dbHelper;

        public LateComingApplicationDataService(IConfiguration configuration)
        {
            _configuration = configuration;
            OracleDBConnection = new OracleDBConnection(configuration);
            dbHelper = new DBHelper(configuration);
        }

        public List<LateCause> GetLateCauseServiceData(string UserID)
        {
            OracleConnection Oracleconnection = new OracleConnection(OracleDBConnection.ConnectionStringRead());

            var lateCauseList = new List<LateCause>();

            try
            {
                OracleCommand objCmd = new OracleCommand("PKG_ATDF_LRE01.F_LATE_CAUSE_LIST", Oracleconnection);
                DataTable dt = new DataTable();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("P_EMP_CODE", OracleDbType.Varchar2).Value = UserID;
                objCmd.Parameters["P_EMP_CODE"].Direction = ParameterDirection.Input;


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
                    lateCauseList.Add(new LateCause
                    {
                        Late_cause_name = row["LT_CAUSE_NAME"].ToString(),
                        Late_cause_code = row["LT_CAUSE_CODE"].ToString()
                    });
                }

                Oracleconnection.Close();
                Oracleconnection.Dispose();
                return lateCauseList;
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

        public List<PndLateApplication> GetPndLateApplicationListServiceData(string UserID, string Year)
        {
            OracleConnection Oracleconnection = new OracleConnection(OracleDBConnection.ConnectionStringRead());

            var pndLateAplnList = new List<PndLateApplication>();

            try
            {
                OracleCommand objCmd = new OracleCommand("PKG_ATDF_LRE01.F_LT_PEND_LIST_DISPLAY", Oracleconnection);
                DataTable dt = new DataTable();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("P_EMP_CODE", OracleDbType.Varchar2).Value = UserID;
                objCmd.Parameters.Add("P_YEAR", OracleDbType.Varchar2).Value = Year;


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
                    pndLateAplnList.Add(new PndLateApplication
                    {
                        application_date = row["LT_APLN_DATE"].ToString(),
                        late_date = row["LT_DATE"].ToString(),
                        expected_in_time = row["EXPECTED_IN_TIME"].ToString(),
                        expected_late_in_time = row["EXPECTED_LT_IN_MIN"].ToString(),
                        late_apln_status = row["LT_APLN_STATUS"].ToString(),
                        late_cause_name = row["LT_CAUSE_NAME"].ToString(),
                        late_apln_status_details = row["LT_APLN_STATUS_DETAILS"].ToString()
                    });
                }

                Oracleconnection.Close();
                Oracleconnection.Dispose();
                return pndLateAplnList;
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

        public List<LateComingHistory> GetPreviousHistoryServiceData(string UserID, string Year)
        {
            OracleConnection Oracleconnection = new OracleConnection(OracleDBConnection.ConnectionStringRead());

            var lateComingHistoryList = new List<LateComingHistory>();

            try
            {
                OracleCommand objCmd = new OracleCommand("PKG_ATDF_LRE01.F_LT_HISTY_DISPLAY", Oracleconnection);
                DataTable dt = new DataTable();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("P_EMP_CODE", OracleDbType.Varchar2).Value = UserID;
                objCmd.Parameters.Add("P_YEAR", OracleDbType.Varchar2).Value = Year;


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
                    lateComingHistoryList.Add(new LateComingHistory
                    {
                        late_detail = row["LT_DETAIL"].ToString(),
                        late_date = row["LT_DATE"].ToString(),
                        expected_in_time = row["EXPECTED_IN_TIME"].ToString(),
                        expected_late_in_time = row["EXPECTED_LT_IN_MIN"].ToString()
                    });
                }

                Oracleconnection.Close();
                Oracleconnection.Dispose();
                return lateComingHistoryList;
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

        public Response_data SubmitApplicationServiceData(SubmitData submitData)
        {
            string status = string.Empty, message = string.Empty /*statusandmessage = string.Empty*/;
            var statusandmessage = new Response_data();


            int expected_late_in_min = Int16.Parse(submitData.expected_late_in_min);
            DateTime dateTime = DateTime.ParseExact(submitData.expected_in_time, "HH:mm", null);
            submitData.expected_in_time = dateTime.ToString("HH:mm:ss");
            // Format the DateTime object to include seconds


            using (OracleConnection connection = new OracleConnection(OracleDBConnection.ConnectionStringRead()))
            {
                // create a new OracleCommand object
                using (OracleCommand command = new OracleCommand("PKG_ATDF_LRE01.P_LT_APLN_SAVE", connection))
                {
                    try
                    {
                        // set the command type to stored procedure
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        // add any parameters the stored procedure requires

                        command.Parameters.Add("P_EMP_CODE", OracleDbType.NVarchar2).Value = submitData.UserID;
                        command.Parameters.Add("P_LT_CAUSE_CODE", OracleDbType.NVarchar2).Value = submitData.late_cause_code;
                        command.Parameters.Add("P_YEAR", OracleDbType.NVarchar2).Value = submitData.year;
                        command.Parameters.Add("P_LT_DATE", OracleDbType.NVarchar2).Value = submitData.late_date;
                        command.Parameters.Add("P_EXPECTED_IN_TIME", OracleDbType.NVarchar2).Value = submitData.expected_in_time;
                        command.Parameters.Add("P_EXPECTED_LT_IN_MIN", OracleDbType.Int32).Value = submitData.expected_late_in_min;
                        command.Parameters.Add("P_VISIT_PLACE", OracleDbType.NVarchar2).Value = "";
                        command.Parameters.Add("P_LT_REMARKS", OracleDbType.NVarchar2).Value = submitData.remarks;
                        command.Parameters.Add("P_NEXT_SUPV_EMP_CODE", OracleDbType.NVarchar2).Value = submitData.next_supv_emcode;
                        command.Parameters.Add("P_LT_APLN_STATUS", OracleDbType.NVarchar2).Value = submitData.late_apln_status;




                        command.Parameters.Add("P_ENTERED_BY", OracleDbType.NVarchar2).Value = submitData.UserID;
                        command.Parameters.Add("P_WORK_STATION", OracleDbType.NVarchar2).Value = Environment.MachineName;
                        command.Parameters.Add("P_DML_STATUS", OracleDbType.NVarchar2).Value = submitData.dml_status;

                        command.Parameters.Add("P_OUT", OracleDbType.Int32, 100).Direction = ParameterDirection.Output;
                        command.BindByName = true;

                        connection.Open();
                        command.ExecuteNonQuery();

                        string returnValue = Convert.ToString(command.Parameters["P_OUT"].Value);

                        statusandmessage = dbHelper.GetStatusAndMessage("ATDF_LRE01", returnValue.ToString());
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
