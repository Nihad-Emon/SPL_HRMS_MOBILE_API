using HRMS_API.DataServices.Interface;
using HRMS_API.Models.EarlyOutRecomApv;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using static HRMS_API.DataServices.DBHelper;

namespace HRMS_API.DataServices.Class
{
    public class EarlyOutRecomApvDataService : IEarlyOutRecomApvDataService
    {
        readonly IConfiguration _configuration;
        private readonly OracleDBConnection OracleDBConnection;
        private readonly DBHelper dbHelper;

        public EarlyOutRecomApvDataService(IConfiguration configuration)
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
                pack = "PKG_ATDF_ERE02.PRCS_EO_SUMMARY_RCMND_DIS";
            }
            else if (recomOrApv == "apv")
            {
                pack = "PKG_ATDF_ERE02.PRCS_EO_SUMMARY_APV_DIS";
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
                            EarlyOut_recom_allow = row["EO_RCMD_ALLOW"].ToString(),
                            EarlyOut_aprv_allow = row["EO_APRV_ALLOW"].ToString()
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
                pack = "PKG_ATDF_ERE02.F_EO_RECMD_DISPLAY";
            }
            else if (recomOrApv == "apv")
            {
                pack = "PKG_ATDF_ERE02.F_EO_APRV_DISPLAY";
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
                            ATD_EO_CODE = row["ATD_EO_CODE"].ToString(),
                            EMP_CODE = row["EMP_CODE"].ToString(),
                            EMP_ID = row["EMP_ID"].ToString(),
                            EMP_NAME = row["EMP_NAME"].ToString(),
                            DEPT_NAME = row["DEPT_NAME"].ToString(),
                            EMP_DESIG_NAME = row["EMP_DESIG_NAME"].ToString(),
                            EO_APLN_DATE = row["EO_APLN_DATE"].ToString(),
                            EO_CAUSE_NAME = row["EO_CAUSE_NAME"].ToString(),
                            EO_DATE = row["EO_DATE"].ToString(),
                            EXPECTED_EO_TIME = row["EXPECTED_EO_TIME"].ToString(),
                            EXPECTED_RETURN_TIME = row["EXPECTED_RETURN_TIME"].ToString(),
                            VISIT_PLACE = row["VISIT_PLACE"].ToString(),
                            RCMD_EMP_NAME = row["RCMD_EMP_NAME"].ToString(),
                            APRV_EMP_NAME = row["APRV_EMP_NAME"].ToString(),
                            EO_RCMD_STUS = row["EO_RCMD_STUS"].ToString(),
                            EO_APLN_STATUS = row["EO_APLN_STATUS"].ToString(),
                            NOTE_FROM_LAST_LAYER = row["NOTE_FROM_LAST_LAYER"].ToString(),
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
                            ATD_EO_CODE = row["ATD_EO_CODE"].ToString(),
                            EMP_CODE = row["EMP_CODE"].ToString(),
                            EMP_ID = row["EMP_ID"].ToString(),
                            EMP_NAME = row["EMP_NAME"].ToString(),
                            DEPT_NAME = row["DEPT_NAME"].ToString(),
                            EMP_DESIG_NAME = row["EMP_DESIG_NAME"].ToString(),
                            EO_APLN_DATE = row["EO_APLN_DATE"].ToString(),
                            EO_CAUSE_NAME = row["EO_CAUSE_NAME"].ToString(),
                            EO_DATE = row["EO_DATE"].ToString(),
                            EXPECTED_EO_TIME = row["EXPECTED_EO_TIME"].ToString(),
                            EXPECTED_RETURN_TIME = row["EXPECTED_RETURN_TIME"].ToString(),
                            VISIT_PLACE = row["VISIT_PLACE"].ToString(),
                            RCMD_EMP_NAME = row["RCMD_EMP_NAME"].ToString(),
                            APRV_EMP_NAME = row["APRV_EMP_NAME"].ToString(),
                            EO_RCMD_STUS = row["EO_RCMD_STUS"].ToString(),
                            EO_APLN_STATUS = row["EO_APLN_STATUS"].ToString(),
                            NOTE_FROM_LAST_LAYER = row["NOTE_FROM_LAST_LAYER"].ToString(),
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
                            ATD_EO_CODE = row["ATD_EO_CODE"].ToString(),
                            EMP_CODE = row["EMP_CODE"].ToString(),
                            EMP_ID = row["EMP_ID"].ToString(),
                            EMP_NAME = row["EMP_NAME"].ToString(),
                            DEPT_NAME = row["DEPT_NAME"].ToString(),
                            EMP_DESIG_NAME = row["EMP_DESIG_NAME"].ToString(),
                            EO_APLN_DATE = row["EO_APLN_DATE"].ToString(),
                            EO_CAUSE_NAME = row["EO_CAUSE_NAME"].ToString(),
                            EO_DATE = row["EO_DATE"].ToString(),
                            EXPECTED_EO_TIME = row["EXPECTED_EO_TIME"].ToString(),
                            EXPECTED_RETURN_TIME = row["EXPECTED_RETURN_TIME"].ToString(),
                            VISIT_PLACE = row["VISIT_PLACE"].ToString(),
                            RCMD_EMP_NAME = row["RCMD_EMP_NAME"].ToString(),
                            APRV_EMP_NAME = row["APRV_EMP_NAME"].ToString(),
                            EO_RCMD_STUS = row["EO_RCMD_STUS"].ToString(),
                            EO_APLN_STATUS = row["EO_APLN_STATUS"].ToString(),
                            NOTE_FROM_LAST_LAYER = row["NOTE_FROM_LAST_LAYER"].ToString(),
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
                pack = "PKG_ATDF_ERE02.PRCS_EO_RECOMMEND";
            }
            else if (infoList[0].recomOrApv == "recom_Rej")
            {
                pack = "PKG_ATDF_ERE02.PRCS_EO_REJECT";
            }
            else if (infoList[0].recomOrApv == "apv")
            {
                pack = "PKG_ATDF_ERE02.PRCS_EO_APPROVE";
            }
            else if (infoList[0].recomOrApv == "apv_Rej")
            {
                pack = "PKG_ATDF_ERE02.PRCS_EO_NOT_APPROVE";
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
                            command.Parameters.Add("P_EO_APLN_EMP_CODE", OracleDbType.NVarchar2).Value = infoList[i].EMP_CODE;
                            command.Parameters.Add("P_ATD_EO_CODE", OracleDbType.NVarchar2).Value = infoList[i].ATD_EO_CODE;
                            command.Parameters.Add("P_COMP_CODE", OracleDbType.NVarchar2).Value = infoList[i].comp_code;
                            command.Parameters.Add("P_NOTE", OracleDbType.NVarchar2).Value = infoList[i].note;

                            command.Parameters.Add("p_out", OracleDbType.Int32, 100).Direction = ParameterDirection.Output;
                            command.BindByName = true;
                            command.ExecuteNonQuery();
                            int returnValue = Convert.ToInt32(command.Parameters["p_out"].Value.ToString());

                            statusandmessage = dbHelper.GetStatusAndMessage("ATDF_ERE02", returnValue.ToString());
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
