using HRMS_API.DataServices.Interface;
using HRMS_API.Models;
using Oracle.ManagedDataAccess.Client;
using static HRMS_API.DataServices.DBHelper;

namespace HRMS_API.DataServices.Class
{
    public class ChangePassDataService : IChangePassDataService
    {
        readonly IConfiguration _configuration;
        private readonly OracleDBConnection OracleDBConnection;

        public ChangePassDataService(IConfiguration configuration)
        {
            _configuration = configuration;
            OracleDBConnection = new OracleDBConnection(configuration);
        }

        public Response_data ChangePassServiceData(SecUser objUser, string oldPassword, string action)
        {
            OracleConnection OraComSrcLstcon = new OracleConnection(OracleDBConnection.ConnectionStringRead());

            string errorNumber = String.Empty;
            var statusandmessage = new Response_data();
            string password = "";

            string OracleComSrcLstQuery = "SELECT pass_encryption12(PASS_WORD) FROM SIL_USER WHERE USER_NAME='" + objUser.User_name + "'";
            try
            {
                OraComSrcLstcon.Open();
                OracleCommand OracleComSrLstcmd = new OracleCommand(OracleComSrcLstQuery, OraComSrcLstcon);

                using (OracleDataReader reader = OracleComSrLstcmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Fetch the data from the selected column
                        password = reader.GetString(0); // Change the index if necessary
                                                        // Do something with the columnValue
                    }
                }
            }
            catch
            {
                OraComSrcLstcon.Close();
                OraComSrcLstcon.Dispose();
                throw;
            }
            finally
            {
                OraComSrcLstcon.Close();
                OraComSrcLstcon.Dispose();
            }
            if (password == oldPassword)
            {
                OracleConnection OraComSrcLstcon1 = new OracleConnection(OracleDBConnection.ConnectionStringRead());
                string Qry = "UPDATE SIL_USER SET PASS_WORD= PASS_ENCRYPTION('" + objUser.User_pass + "') WHERE USER_NAME= '" + objUser.User_name + "' ";

                try
                {
                    OraComSrcLstcon1.Open();
                    OracleCommand OracleComSrLstcmd1 = new OracleCommand(Qry, OraComSrcLstcon1);
                    int rowsUpdated = OracleComSrLstcmd1.ExecuteNonQuery();
                    if (rowsUpdated == 1)
                    {
                        errorNumber = "Password Changed Successfully";
                        statusandmessage.status = "1";
                        statusandmessage.message = errorNumber;
                        statusandmessage.msg_type = "M";
                    }
                    else
                    {
                        errorNumber = "Password change Unsuccessfull";
                        statusandmessage.status = "0";
                        statusandmessage.message = errorNumber;
                        statusandmessage.msg_type = "E";

                    }
                }
                catch (Exception ex)
                {
                    errorNumber = "E404";
                    OraComSrcLstcon1.Close();
                    OraComSrcLstcon1.Dispose();
                    throw;// Log ex.Message  Insert Log Table               
                }
                finally
                {
                    OraComSrcLstcon1.Close();
                    OraComSrcLstcon1.Dispose();
                }
            }
            else
            {
                errorNumber = "Old password isn't  correct";
                statusandmessage.status = "0";
                statusandmessage.message = errorNumber;
                statusandmessage.msg_type = "E";
            }
            return statusandmessage;
        }
    }
}
