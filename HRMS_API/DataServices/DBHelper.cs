using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace HRMS_API.DataServices
{
    public class DBHelper
    {
        readonly IConfiguration _configuration;
        private readonly OracleDBConnection OracleDBConnection;

        public DBHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            OracleDBConnection = new OracleDBConnection(configuration);
        }
        public DataTable GetDataTable(string Qry)
        {
            OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(Qry, OracleDBConnection.ConnectionStringRead());
            DataTable dt = new DataTable();
            oracleDataAdapter.Fill(dt);
            return dt;
        }

        public int CmdExecute(string Qry)
        {
            int noOfRows = 0;
            using (OracleConnection con = new OracleConnection(OracleDBConnection.ConnectionStringRead()))
            {
                OracleCommand cmd = new OracleCommand(Qry, con);
                con.Open();
                noOfRows = cmd.ExecuteNonQuery();
            }
            return noOfRows;
        }
        public Response_data GetStatusAndMessage(string fromName, string outParam)
        {
            //string status = string.Empty, message = string.Empty , msg_type = string.Empty;
            var response_data = new Response_data();
            try
            {
                using (OracleConnection connection = new OracleConnection(OracleDBConnection.ConnectionStringRead()))
                {
                    string OraMsgStsQuery = "SELECT msg_code, msg_value , MSG_TYPE  FROM HRMF_FORM_MSG WHERE submenu_id =" + "'" + fromName + "'" + " AND msg_code= '" + outParam + "'";
                    OracleCommand OracleMsgStscmd = new OracleCommand(OraMsgStsQuery, connection);

                    OracleDataAdapter OracleMDa = new OracleDataAdapter(OracleMsgStscmd);
                    DataTable OracleMDt = new DataTable();
                    OracleMDa.Fill(OracleMDt);
                    if (OracleMDt.Rows.Count > 0)
                    {
                        response_data.status = OracleMDt.Rows[0]["msg_code"].ToString();
                        response_data.message = OracleMDt.Rows[0]["msg_value"].ToString();
                        response_data.msg_type = OracleMDt.Rows[0]["MSG_TYPE"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                response_data.status = "101";
                response_data.message = ex.InnerException.Message.ToString();
            }
            return response_data;
        }

        public class Response_data
        {
            public string? status { get; set; }
            public string? message { get; set; }
            public string? msg_type { get; set; }
        }
    
}
}
