using HRMS_API.DataServices.Interface;
using HRMS_API.Models.Outoffice;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using static HRMS_API.DataServices.DBHelper;
using System.Data;

namespace HRMS_API.DataServices.Class
{
    public class OutofficeApplicationDataService : IOutofficeApplicationDataService
    {
        readonly IConfiguration _configuration;
        private readonly OracleDBConnection OracleDBConnection;
        private readonly DBHelper dbHelper;


        public OutofficeApplicationDataService(IConfiguration configuration)
        {
            _configuration = configuration;
            OracleDBConnection = new OracleDBConnection(configuration);
            dbHelper = new DBHelper(configuration);
        }

        public List<PndLeaveApplication> GetPndAplnListServiceData(string UserID, string Year)
        {
            OracleConnection Oracleconnection = new OracleConnection(OracleDBConnection.ConnectionStringRead());

            var pndLeaveApplicationList = new List<PndLeaveApplication>();

            try
            {
                OracleCommand objCmd = new OracleCommand("PKG_ATDF_ODE01.F_OUTOFF_PEND_LIST_DISPLAY", Oracleconnection);
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
                    pndLeaveApplicationList.Add(new PndLeaveApplication
                    {

                        Application_date = row["OFD_APLN_DATE"].ToString(),
                        From_date = row["OFD_START_DATE"].ToString(),
                        To_date = row["OFD_END_DATE"].ToString(),
                        Sttime = row["OFD_START_TIME"].ToString(),
                        Endtime = row["OFD_END_TIME"].ToString(),
                        Days = row["OFD_DAYS"].ToString(),
                        Duty_Place = row["OUTOFF_PLACE"].ToString(),
                        ColorStatus = row["OFD_APLN_STATUS"].ToString(),
                        Cause = row["OUTOFF_REASON"].ToString(),
                        Status = row["OFD_APLN_STATUS_DETAILS"].ToString()
                    });
                }

                Oracleconnection.Close();
                Oracleconnection.Dispose();
                return pndLeaveApplicationList;
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

        public List<OutofficeHistory> GetPreviousHistoryServiceData(string UserID, string Year)
        {
            OracleConnection Oracleconnection = new OracleConnection(OracleDBConnection.ConnectionStringRead());

            var outofficeHistories = new List<OutofficeHistory>();

            try
            {
                OracleCommand objCmd = new OracleCommand("PKG_ATDF_ODE01.F_OUTOFF_HISTY_DISPLAY", Oracleconnection);
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
                    outofficeHistories.Add(new OutofficeHistory
                    {

                        SL = Convert.ToInt16(row["OUTOFF_CODE"].ToString()),
                        Leave_days = row["OFD_DAYS"].ToString(),
                        Leave_detail = row["OFD_DETAIL"].ToString(),
                        Start_Date = row["OFD_START_DATE"].ToString(),
                        End_Date = row["OFD_END_DATE"].ToString(),
                        Start_Time = row["OFD_START_TIME"].ToString(),
                        End_Time = row["OFD_END_TIME"].ToString()
                    });
                }

                Oracleconnection.Close();
                Oracleconnection.Dispose();
                return outofficeHistories;
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

            int leave_days = Int16.Parse(submitData.leav_takn_days);
            DateTime dateTime = DateTime.ParseExact(submitData.leav_from_time, "HH:mm", null);
            submitData.leav_from_time = dateTime.ToString("HH:mm:ss");
            // Format the DateTime object to include seconds

            DateTime dateTime2 = DateTime.ParseExact(submitData.leav_to_time, "HH:mm", null);
            submitData.leav_to_time = dateTime2.ToString("HH:mm:ss");

            using (OracleConnection connection = new OracleConnection(OracleDBConnection.ConnectionStringRead()))
            {
                // create a new OracleCommand object
                using (OracleCommand command = new OracleCommand("PKG_ATDF_ODE01.P_OUTOFF_APLN_SAVE", connection))
                {
                    try
                    {
                        // set the command type to stored procedure
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        // add any parameters the stored procedure requires

                        command.Parameters.Add("P_EMP_CODE", OracleDbType.NVarchar2).Value = submitData.UserID;
                        command.Parameters.Add("P_OFD_TYPE_CODE", OracleDbType.NVarchar2).Value = "HM";
                        command.Parameters.Add("P_YEAR", OracleDbType.NVarchar2).Value = submitData.year;
                        command.Parameters.Add("P_OFD_START_DATE", OracleDbType.NVarchar2).Value = submitData.leav_from_date;
                        command.Parameters.Add("P_OFD_END_DATE", OracleDbType.NVarchar2).Value = submitData.leav_to_date;
                        command.Parameters.Add("P_OFD_START_TIME", OracleDbType.NVarchar2).Value = submitData.leav_from_time;
                        command.Parameters.Add("P_OFD_END_TIME", OracleDbType.NVarchar2).Value = submitData.leav_to_time;
                        command.Parameters.Add("P_DUTY_DAYS", OracleDbType.Int32).Value = leave_days;

                        command.Parameters.Add("P_OFD_DUTY_PLACE", OracleDbType.NVarchar2).Value = submitData.duty;
                        command.Parameters.Add("P_OFD_REASON", OracleDbType.NVarchar2).Value = submitData.leav_takn_caus;
                        command.Parameters.Add("P_OFD_REMARKS", OracleDbType.NVarchar2).Value = submitData.Remarks;
                        command.Parameters.Add("P_NEXT_SUPV_EMP_CODE", OracleDbType.NVarchar2).Value = submitData.next_supv_emcode;
                        command.Parameters.Add("P_OFD_APLN_STATUS", OracleDbType.NVarchar2).Value = submitData.leav_apln_status;



                        command.Parameters.Add("P_ENTERED_BY", OracleDbType.NVarchar2).Value = submitData.UserID;
                        command.Parameters.Add("P_WORK_STATION", OracleDbType.NVarchar2).Value = Environment.MachineName;
                        command.Parameters.Add("P_DML_STATUS", OracleDbType.NVarchar2).Value = submitData.dml_status;

                        command.Parameters.Add("P_OUT", OracleDbType.Int32, 100).Direction = ParameterDirection.Output;
                        command.BindByName = true;

                        connection.Open();
                        command.ExecuteNonQuery();

                        string returnValue = Convert.ToString(command.Parameters["p_out"].Value);

                        statusandmessage = dbHelper.GetStatusAndMessage("ATDF_ODE01", returnValue.ToString());
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
