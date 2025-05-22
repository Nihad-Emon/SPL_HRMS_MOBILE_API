using HRMS_API.DataServices.Interface;
using HRMS_API.Models.OutOfficeRecomApv;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using static HRMS_API.DataServices.DBHelper;

namespace HRMS_API.DataServices.Class
{
    public class OutofficeRecomApproveDataService : IOutofficeRecomApproveDataService
    {
        readonly IConfiguration _configuration;
        private readonly OracleDBConnection OracleDBConnection;
        private readonly DBHelper dbHelper;

        public OutofficeRecomApproveDataService(IConfiguration configuration)
        {
            _configuration = configuration;
            OracleDBConnection = new OracleDBConnection(configuration);
            dbHelper = new DBHelper(configuration);
        }

        public List<RecomApv> GetRecomApvListServiceData(string year, string toBrowse, string recomOrApv, string UserID)
        {
            OracleConnection Oracleconnection = new OracleConnection(OracleDBConnection.ConnectionStringRead());

            var recomapvList = new List<RecomApv>();
            string pack = "";

            if (recomOrApv == "recom")
            {
                pack = "PKG_ATDF_ODE02.PRCS_OFD_SUMMARY_RCMND_DIS";
            }
            else if (recomOrApv == "apv")
            {
                pack = "PKG_ATDF_ODE02.PRCS_OFD_SUMMARY_APV_DIS";
            }
            else
            {
            }

            try
            {
                OracleCommand objCmd = new OracleCommand(pack, Oracleconnection);
                DataTable dt = new DataTable();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("P_EMP_CODE", OracleDbType.Varchar2).Value = UserID;
                objCmd.Parameters.Add("P_YEAR", OracleDbType.Varchar2).Value = year;
                objCmd.Parameters.Add("P_CHCK_FLG", OracleDbType.Varchar2).Value = toBrowse;


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
                if (recomOrApv == "recom")
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        recomapvList.Add(new RecomApv
                        {
                            Comp_name = row["COMP_NAME"].ToString(),
                            Comp_code = row["COMP_CODE"].ToString(),
                            Pend_for_recom = row["PND_FOR_RCMD"].ToString(),
                            Pending_title = row["PENDING_TITLE"].ToString(),
                            Outoff_recom_allow = row["OFD_RCMD_ALLOW"].ToString(),
                            Outoff_aprv_allow = row["OFD_APRV_ALLOW"].ToString()
                        });
                    }
                }
                else if (recomOrApv == "apv")
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        recomapvList.Add(new RecomApv
                        {
                            Comp_name = row["COMP_NAME"].ToString(),
                            Comp_code = row["COMP_CODE"].ToString(),
                            Pnd_for_aprv = row["PND_FOR_APRV"].ToString(),
                            Pending_title = row["PENDING_TITLE"].ToString()
                        });
                    }
                }
                else
                {
                }
                Oracleconnection.Close();
                Oracleconnection.Dispose();
                return recomapvList;
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

        public List<RecomApvData> GetRecomApvTableServiceData(string year, string comp_code, string toBrowse, string recomOrApv, string UserID)
        {
            OracleConnection Oracleconnection = new OracleConnection(OracleDBConnection.ConnectionStringRead());

            var recomApvTableDataList = new List<RecomApvData>();

            string pack = "";
            if (recomOrApv == "recom")
            {
                pack = "PKG_ATDF_ODE02.F_OFD_RECMD_DISPLAY";
            }
            else if (recomOrApv == "apv")
            {
                pack = "PKG_ATDF_ODE02.F_OFD_APRV_DISPLAY";
            }

            try
            {
                OracleCommand objCmd = new OracleCommand(pack, Oracleconnection);
                DataTable dt = new DataTable();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("P_EMP_CODE", OracleDbType.Varchar2).Value = UserID;
                objCmd.Parameters.Add("P_YEAR", OracleDbType.Varchar2).Value = year;
                objCmd.Parameters.Add("P_COMP_CODE", OracleDbType.Varchar2).Value = comp_code;
                objCmd.Parameters.Add("P_CHCK_FLG", OracleDbType.Varchar2).Value = toBrowse;


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
                if (toBrowse == "P" && recomOrApv == "recom")
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        recomApvTableDataList.Add(new RecomApvData
                        {

                            OUTOFF_CODE = row["OUTOFF_CODE"].ToString(),
                            EMP_CODE = row["EMP_CODE"].ToString(),
                            EMP_ID = row["EMP_ID"].ToString(),
                            EMP_NAME = row["EMP_NAME"].ToString(),
                            DEPT_NAME = row["DEPT_NAME"].ToString(),
                            EMP_DESIG_NAME = row["EMP_DESIG_NAME"].ToString(),
                            OFD_TYPE_NAME = row["OFD_TYPE_NAME"].ToString(),
                            OFD_START_DATE = row["OFD_START_DATE"].ToString(),
                            OFD_END_DATE = row["OFD_END_DATE"].ToString(),
                            OFD_DAYS = row["OFD_DAYS"].ToString(),
                            OUTOFF_REASON = row["OUTOFF_REASON"].ToString(),
                            NEXT_SUPV_EMP_CODE = row["NEXT_SUPV_EMP_CODE"].ToString(),
                            RCMD_EMP_NAME = row["RCMD_EMP_NAME"].ToString(),
                            APRV_EMP_NAME = row["APRV_EMP_NAME"].ToString(),
                            OFD_APLN_STATUS = row["OFD_APLN_STATUS"].ToString(),
                            NOTE_FROM_LAST_LAYER = row["NOTE_FROM_LAST_LAYER"].ToString(),
                            APPLICATION_DATE = row["APPLICATION_DATE"].ToString(),
                            OFD_START_TIME = row["OFD_START_TIME"].ToString(),
                            OFD_END_TIME = row["OFD_END_TIME"].ToString(),
                            DESTINATION = row["DESTINATION"].ToString(),
                            PndOrApv = "Recom"
                        });
                    }
                }

                else if (toBrowse == "P" && recomOrApv == "apv")
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        recomApvTableDataList.Add(new RecomApvData
                        {
                            OUTOFF_CODE = row["OUTOFF_CODE"].ToString(),
                            EMP_CODE = row["EMP_CODE"].ToString(),
                            EMP_ID = row["EMP_ID"].ToString(),
                            EMP_NAME = row["EMP_NAME"].ToString(),
                            DEPT_NAME = row["DEPT_NAME"].ToString(),
                            EMP_DESIG_NAME = row["EMP_DESIG_NAME"].ToString(),
                            OFD_TYPE_NAME = row["OFD_TYPE_NAME"].ToString(),
                            OFD_START_DATE = row["OFD_START_DATE"].ToString(),
                            OFD_END_DATE = row["OFD_END_DATE"].ToString(),
                            OFD_DAYS = row["OFD_DAYS"].ToString(),
                            OUTOFF_REASON = row["OUTOFF_REASON"].ToString(),
                            NEXT_SUPV_EMP_CODE = row["NEXT_SUPV_EMP_CODE"].ToString(),
                            RCMD_EMP_NAME = row["RCMD_EMP_NAME"].ToString(),
                            APRV_EMP_NAME = row["APRV_EMP_NAME"].ToString(),
                            OFD_APLN_STATUS = row["OFD_APLN_STATUS"].ToString(),
                            NOTE_FROM_LAST_LAYER = row["NOTE_FROM_LAST_LAYER"].ToString(),
                            APPLICATION_DATE = row["APPLICATION_DATE"].ToString(),
                            OFD_START_TIME = row["OFD_START_TIME"].ToString(),
                            OFD_END_TIME = row["OFD_END_TIME"].ToString(),
                            DESTINATION = row["DESTINATION"].ToString(),
                            PndOrApv = "Apv",
                        });
                    }
                }

                else if (toBrowse == "O")
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        recomApvTableDataList.Add(new RecomApvData
                        {
                            OUTOFF_CODE = row["OUTOFF_CODE"].ToString(),
                            EMP_CODE = row["EMP_CODE"].ToString(),
                            EMP_ID = row["EMP_ID"].ToString(),
                            EMP_NAME = row["EMP_NAME"].ToString(),
                            DEPT_NAME = row["DEPT_NAME"].ToString(),
                            EMP_DESIG_NAME = row["EMP_DESIG_NAME"].ToString(),
                            OFD_TYPE_NAME = row["OFD_TYPE_NAME"].ToString(),
                            OFD_START_DATE = row["OFD_START_DATE"].ToString(),
                            OFD_END_DATE = row["OFD_END_DATE"].ToString(),
                            OFD_DAYS = row["OFD_DAYS"].ToString(),
                            OUTOFF_REASON = row["OUTOFF_REASON"].ToString(),
                            NEXT_SUPV_EMP_CODE = row["NEXT_SUPV_EMP_CODE"].ToString(),
                            RCMD_EMP_NAME = row["RCMD_EMP_NAME"].ToString(),
                            APRV_EMP_NAME = row["APRV_EMP_NAME"].ToString(),
                            OFD_APLN_STATUS = row["OFD_APLN_STATUS"].ToString(),
                            NOTE_FROM_LAST_LAYER = row["NOTE_FROM_LAST_LAYER"].ToString(),
                            APPLICATION_DATE = row["APPLICATION_DATE"].ToString(),
                            OFD_START_TIME = row["OFD_START_TIME"].ToString(),
                            OFD_END_TIME = row["OFD_END_TIME"].ToString(),
                            DESTINATION = row["DESTINATION"].ToString()
                        });
                    }
                }


                Oracleconnection.Close();
                Oracleconnection.Dispose();
                return recomApvTableDataList;
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

        public Response_data SubmitRecomApvServiceData(List<RecomApvData> infoList)
        {
            string status = string.Empty, message = string.Empty;
            var statusandmessage = new Response_data();

            string pack = "";
            if (infoList[0].recomOrApv == "recom")
            {
                pack = "PKG_ATDF_ODE02.PRCS_OFD_RECOMMEND";
            }
            else if (infoList[0].recomOrApv == "recom_Rej")
            {
                pack = "PKG_ATDF_ODE02.PRCS_OFD_REJECT";
            }
            else if (infoList[0].recomOrApv == "apv")
            {
                pack = "PKG_ATDF_ODE02.PRCS_OFD_APPROVE";
            }
            else if (infoList[0].recomOrApv == "apv_Rej")
            {
                pack = "PKG_ATDF_ODE02.PRCS_OFD_NOT_APPROVE";
            }


            for (int i = 0; i < infoList.Count; i++)
            {
                using (OracleConnection connection = new OracleConnection(OracleDBConnection.ConnectionStringRead()))
                {

                    using (OracleCommand command = new OracleCommand(pack, connection))
                    {

                        try
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            connection.Open();

                            command.Parameters.Add("P_EMP_CODE", OracleDbType.NVarchar2).Value = infoList[i].UserID;
                            command.Parameters.Add("P_OFD_APLN_EMP_CODE", OracleDbType.NVarchar2).Value = infoList[i].EMP_CODE;
                            command.Parameters.Add("P_OUTOFF_CODE", OracleDbType.NVarchar2).Value = infoList[i].OUTOFF_CODE;
                            command.Parameters.Add("P_COMP_CODE", OracleDbType.NVarchar2).Value = infoList[i].comp_code;
                            command.Parameters.Add("P_NOTE", OracleDbType.NVarchar2).Value = infoList[i].note;

                            command.Parameters.Add("p_out", OracleDbType.Int32, 100).Direction = ParameterDirection.Output;
                            command.BindByName = true;
                            command.ExecuteNonQuery();
                            int returnValue = Convert.ToInt32(command.Parameters["p_out"].Value.ToString());

                            statusandmessage = dbHelper.GetStatusAndMessage("ATDF_ODE02", returnValue.ToString());
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
            }
            return statusandmessage;
        }
    }
}
