using HRMS_API.DataServices.Interface;
using HRMS_API.Models.EarlyOutApplication;
using HRMS_API.Services.Class;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using static HRMS_API.DataServices.DBHelper;

namespace HRMS_API.DataServices.Class
{
    public class EarlyOutApplicationDataService : IEarlyOutApplicationDataService
    {
        readonly IConfiguration _configuration;
        private readonly OracleDBConnection OracleDBConnection;
        private readonly DBHelper dbHelper;

        public EarlyOutApplicationDataService(IConfiguration configuration)
        {
            _configuration = configuration;
            OracleDBConnection = new OracleDBConnection(configuration);
            dbHelper = new DBHelper(configuration);
        }

        public List<EarlyOutCause> GetEarlyOutCauseServiceData(string UserID)
        {
            OracleConnection Oracleconnection = new OracleConnection(OracleDBConnection.ConnectionStringRead());

            var earlyOutCauseList = new List<EarlyOutCause>();

            try
            {
                OracleCommand objCmd = new OracleCommand("PKG_ATDF_ERE01.F_EO_CAUSE_LIST", Oracleconnection);
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
                    earlyOutCauseList.Add(new EarlyOutCause
                    {
                        EarlyOut_cause_name = row["EO_CAUSE_NAME"].ToString(),
                        EarlyOut_cause_code = row["EO_CAUSE_CODE"].ToString()
                    });
                }

                Oracleconnection.Close();
                Oracleconnection.Dispose();
                return earlyOutCauseList;
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

        public List<PndEarlyOutApplication> GetPndEarlyOutApplicationListServiceData(string UserID, string Year)
        {
            OracleConnection Oracleconnection = new OracleConnection(OracleDBConnection.ConnectionStringRead());

            var pndEarlyOutApplicationList = new List<PndEarlyOutApplication>();

            try
            {
                OracleCommand objCmd = new OracleCommand("PKG_ATDF_ERE01.F_EO_PEND_LIST_DISPLAY", Oracleconnection);
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
                    pndEarlyOutApplicationList.Add(new PndEarlyOutApplication
                    {
                        EarlyOut_apln_date = row["LT_APLN_DATE"].ToString(),
                        EarlyOut_date = row["EO_DATE"].ToString(),
                        Expected_earlyOut_time = row["EXPECTED_EO_TIME"].ToString(),
                        Expected_return_time = row["EXPECTED_RETURN_TIME"].ToString(),
                        EarlyOut_apln_status = row["EO_APLN_STATUS"].ToString(),
                        EarlyOut_cause_name = row["EO_CAUSE_NAME"].ToString(),
                        EarlyOut_apln_status_details = row["EO_APLN_STATUS_DETAILS"].ToString()
                    });
                }

                Oracleconnection.Close();
                Oracleconnection.Dispose();
                return pndEarlyOutApplicationList;
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

        public List<EarlyOutHistory> GetPreviousHistoryServiceData(string UserID, string Year)
        {
            OracleConnection Oracleconnection = new OracleConnection(OracleDBConnection.ConnectionStringRead());

            var earlyOutHistoryList = new List<EarlyOutHistory>();

            try
            {
                OracleCommand objCmd = new OracleCommand("PKG_ATDF_ERE01.F_EO_HISTY_DISPLAY", Oracleconnection);
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
                    earlyOutHistoryList.Add(new EarlyOutHistory
                    {
                        EarlyOut_details = row["EO_DETAIL"].ToString(),
                        EarlyOut_date = row["EO_DATE"].ToString(),
                        EarlyOut_time = row["EXPECTED_EO_TIME"].ToString(),
                        Return_time = row["EXPECTED_RETURN_TIME"].ToString()
                    });
                }

                Oracleconnection.Close();
                Oracleconnection.Dispose();
                return earlyOutHistoryList;
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

            DateTime dateTime = DateTime.ParseExact(submitData.EXPECTED_EO_TIME, "HH:mm", null);
            submitData.EXPECTED_EO_TIME = dateTime.ToString("HH:mm:ss");

            if (submitData.EXPECTED_RETURN_TIME != "")
            {
                DateTime dateTime2 = DateTime.ParseExact(submitData.EXPECTED_RETURN_TIME, "HH:mm", null);
                submitData.EXPECTED_RETURN_TIME = dateTime2.ToString("HH:mm:ss");
            }

            using (OracleConnection connection = new OracleConnection(OracleDBConnection.ConnectionStringRead()))
            {
                // create a new OracleCommand object
                using (OracleCommand command = new OracleCommand("PKG_ATDF_ERE01.P_EO_APLN_SAVE", connection))
                {
                    try
                    {
                        // set the command type to stored procedure
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        // add any parameters the stored procedure requires

                        command.Parameters.Add("P_EMP_CODE", OracleDbType.NVarchar2).Value = submitData.UserID;
                        command.Parameters.Add("P_EO_CAUSE_CODE", OracleDbType.NVarchar2).Value = submitData.EO_CAUSE_CODE;
                        command.Parameters.Add("P_YEAR", OracleDbType.NVarchar2).Value = submitData.YEAR;
                        command.Parameters.Add("P_EO_DATE", OracleDbType.NVarchar2).Value = submitData.EO_DATE;
                        command.Parameters.Add("P_EXPECTED_EO_TIME", OracleDbType.NVarchar2).Value = submitData.EXPECTED_EO_TIME;
                        command.Parameters.Add("P_EXPECTED_RETURN_TIME", OracleDbType.NVarchar2).Value = submitData.EXPECTED_RETURN_TIME;
                        command.Parameters.Add("P_NO_RETURN_FLAG", OracleDbType.NVarchar2).Value = submitData.NO_RETURN_FLAG;
                        command.Parameters.Add("P_VISIT_PLACE", OracleDbType.NVarchar2).Value = submitData.VISIT_PLACE;
                        command.Parameters.Add("P_EO_REMARKS", OracleDbType.NVarchar2).Value = submitData.EO_REMARKS;
                        command.Parameters.Add("P_NEXT_SUPV_EMP_CODE", OracleDbType.NVarchar2).Value = submitData.NEXT_SUPV_EMP_CODE;
                        command.Parameters.Add("P_EO_APLN_STATUS", OracleDbType.NVarchar2).Value = submitData.EO_APLN_STATUS;
                        command.Parameters.Add("P_ENTERED_BY", OracleDbType.NVarchar2).Value = submitData.UserID;
                        command.Parameters.Add("P_WORK_STATION", OracleDbType.NVarchar2).Value = Environment.MachineName;
                        command.Parameters.Add("P_DML_STATUS", OracleDbType.NVarchar2).Value = submitData.DML_STATUS;

                        command.Parameters.Add("P_OUT", OracleDbType.Int32, 100).Direction = ParameterDirection.Output;
                        command.BindByName = true;

                        connection.Open();
                        command.ExecuteNonQuery();

                        string returnValue = Convert.ToString(command.Parameters["P_OUT"].Value);

                        statusandmessage = dbHelper.GetStatusAndMessage("ATDF_ERE01", returnValue.ToString());
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
