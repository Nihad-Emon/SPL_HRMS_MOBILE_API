using Oracle.ManagedDataAccess.Client;

namespace HRMS_API.DataServices
{
    public class OracleDBConnection
    {
        private readonly IConfiguration _configuration;

        public OracleDBConnection(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ConnectionStringRead()
        {
            string connectionString = _configuration.GetConnectionString("databseConnection");
            return connectionString;
        }
    }
}
