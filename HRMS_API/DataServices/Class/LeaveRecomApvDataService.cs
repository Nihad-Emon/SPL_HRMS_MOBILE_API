using HRMS_API.DataServices.Interface;
using HRMS_API.Models.Home;
using HRMS_API.Models.RecomApprove;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using static HRMS_API.DataServices.DBHelper;

namespace HRMS_API.DataServices.Class
{
    public class LeaveRecomApvDataService : ILeaveRecomApvDataService
    {
        readonly IConfiguration _configuration;
        private readonly OracleDBConnection OracleDBConnection;
        private readonly DBHelper dbHelper;

        public LeaveRecomApvDataService(IConfiguration configuration)
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
                pack = "pkg01_hrms_lap02.prcs_leave_summary_rcmnd_dis";
            }
            else if (recomOrApv == "apv")
            {
                pack = "pkg01_hrms_lap02.prcs_leave_summary_apv_dis";
            }
            else
            {
            }

            try
            {
                OracleCommand objCmd = new OracleCommand(pack, Oracleconnection);
                DataTable dt = new DataTable();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("p_emp_code", OracleDbType.Varchar2).Value = UserID;
                objCmd.Parameters["p_emp_code"].Direction = ParameterDirection.Input;
                objCmd.Parameters.Add("p_leave_year", OracleDbType.Varchar2).Value = year;
                objCmd.Parameters["p_leave_year"].Direction = ParameterDirection.Input;
                objCmd.Parameters.Add("p_chck_flg", OracleDbType.Varchar2).Value = toBrowse;
                objCmd.Parameters["p_chck_flg"].Direction = ParameterDirection.Input;


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
                            Comp_name = row["comp_name"].ToString(),
                            Comp_code = row["comp_code"].ToString(),
                            Pend_for_recom = row["pnd_for_rcmd"].ToString(),
                            Pending_title = row["Pending_title"].ToString(),
                            Leav_recom_allow = row["leav_rcmd_allow"].ToString(),
                            Leav_aprv_allow = row["leav_aprv_allow"].ToString(),
                        });
                    }
                }
                else if (recomOrApv == "apv")
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        recomapvList.Add(new RecomApv
                        {
                            Comp_name = row["comp_name"].ToString(),
                            Comp_code = row["comp_code"].ToString(),
                            Pnd_for_aprv = row["pnd_for_aprv"].ToString(),
                            Pending_title = row["Pending_title"].ToString()
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
                pack = "pkg01_hrms_lap02.f_leav_recmd_display";
            }
            else if (recomOrApv == "apv")
            {
                pack = "pkg01_hrms_lap02.f_leav_aprv_display";
            }

            try
            {
                OracleCommand objCmd = new OracleCommand(pack, Oracleconnection);
                DataTable dt = new DataTable();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("p_emp_code", OracleDbType.Varchar2).Value = UserID;
                objCmd.Parameters["p_emp_code"].Direction = ParameterDirection.Input;
                objCmd.Parameters.Add("p_leav_year", OracleDbType.Varchar2).Value = year;
                objCmd.Parameters["p_leav_year"].Direction = ParameterDirection.Input;
                objCmd.Parameters.Add("p_comp_code", OracleDbType.Varchar2).Value = comp_code;
                objCmd.Parameters["p_comp_code"].Direction = ParameterDirection.Input;
                objCmd.Parameters.Add("p_chck_flg", OracleDbType.Varchar2).Value = toBrowse;
                objCmd.Parameters["p_chck_flg"].Direction = ParameterDirection.Input;


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
                            Leav_apln_slno = Convert.ToInt16(row["leav_apln_slno"].ToString()),
                            Emp_id = row["emp_id"].ToString(),
                            Emp_name = row["emp_name"].ToString(),
                            Emp_code = row["emp_code"].ToString(),
                            Dept_name = row["dept_name"].ToString(),
                            Emp_desig_name = row["emp_desig_name"].ToString(),
                            Leav_type_name = row["leav_type_name"].ToString(),
                            Leav_type_code = row["leav_type_code"].ToString(),
                            Leave_catg_code = row["leav_catg_code"].ToString(),
                            Leav_from_date = row["leav_from_date"].ToString(),
                            Leav_to_date = row["leav_to_date"].ToString(),
                            Leav_takn_days = row["leav_takn_days"].ToString(),
                            Leav_takn_caus = row["leav_takn_caus"].ToString(),
                            Leav_days_stay = row["leav_days_stay"].ToString(),
                            Rcmd_emp_name = row["rcmd_emp_name"].ToString(),
                            Aprv_emp_name = row["aprv_emp_name"].ToString(),
                            Leav_apln_status = row["leav_apln_status"].ToString(),
                            Blnc_leav_days = row["blnc_leav_days"].ToString(),
                            Leav_apln_code = row["leav_apln_code"].ToString(),
                            Leav_apln_date = row["leav_apln_date"].ToString(),
                            On_leav_phn_no = row["on_leav_phn_no"].ToString(),
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
                            Leav_apln_slno = Convert.ToInt16(row["leav_apln_slno"].ToString()),
                            Emp_id = row["emp_id"].ToString(),
                            Emp_name = row["emp_name"].ToString(),
                            Emp_code = row["emp_code"].ToString(),
                            Dept_name = row["dept_name"].ToString(),
                            Emp_desig_name = row["emp_desig_name"].ToString(),
                            Leav_type_name = row["leav_type_name"].ToString(),
                            Leav_type_code = row["leav_type_code"].ToString(),
                            Leave_catg_code = row["leav_catg_code"].ToString(),
                            Leav_from_date = row["leav_from_date"].ToString(),
                            Leav_to_date = row["leav_to_date"].ToString(),
                            Leav_takn_days = row["leav_takn_days"].ToString(),
                            Leav_takn_caus = row["leav_takn_caus"].ToString(),
                            Leav_days_stay = row["leav_days_stay"].ToString(),
                            Rcmd_emp_name = row["rcmd_emp_name"].ToString(),
                            Aprv_emp_name = row["aprv_emp_name"].ToString(),
                            Leav_apln_status = row["leav_apln_status"].ToString(),
                            Blnc_leav_days = row["blnc_leav_days"].ToString(),
                            Note_from_last_layer = row["note_from_last_layer"].ToString(),
                            Leav_apln_code = row["leav_apln_code"].ToString(),
                            Leav_apln_date = row["leav_apln_date"].ToString(),
                            On_leav_phn_no = row["on_leav_phn_no"].ToString(),
                            Leav_catg_code = row["leav_catg_code"].ToString(),
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
                            Leav_apln_slno = Convert.ToInt16(row["leav_apln_slno"].ToString()),
                            Emp_id = row["emp_id"].ToString(),
                            Emp_name = row["emp_name"].ToString(),
                            Emp_code = row["emp_code"].ToString(),
                            Dept_name = row["dept_name"].ToString(),
                            Emp_desig_name = row["emp_desig_name"].ToString(),
                            Leav_type_name = row["leav_type_name"].ToString(),
                            Leav_type_code = row["leav_type_code"].ToString(),
                            Leav_from_date = row["leav_from_date"].ToString(),
                            Leav_to_date = row["leav_to_date"].ToString(),
                            Leav_takn_days = row["leav_takn_days"].ToString(),
                            Leav_takn_caus = row["leav_takn_caus"].ToString(),
                            Leav_days_stay = row["leav_days_stay"].ToString(),
                            Rcmd_emp_name = row["rcmd_emp_name"].ToString(),
                            Aprv_emp_name = row["aprv_emp_name"].ToString(),
                            Leav_apln_status = row["leav_apln_status"].ToString(),
                            Blnc_leav_days = row["blnc_leav_days"].ToString(),
                            Leav_apln_code = row["leav_apln_code"].ToString(),
                            Leav_apln_date = row["leav_apln_date"].ToString(),
                            On_leav_phn_no = row["on_leav_phn_no"].ToString(),
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



        public List<Balance> GetBalanceServiceData(string UserID, string Year, string Emp_code, string Comp_code)
        {
            OracleConnection Oracleconnection = new OracleConnection(OracleDBConnection.ConnectionStringRead());

            var leaveBalanceDataList = new List<Balance>();

            try
            {
                OracleCommand objCmd = new OracleCommand("pkg01_hrms_lap02.f_leav_balnc_display", Oracleconnection);
                DataTable dt = new DataTable();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("p_login_emp_code", OracleDbType.Varchar2).Value = UserID;
                objCmd.Parameters.Add("p_emp_code", OracleDbType.Varchar2).Value = Emp_code;
                objCmd.Parameters.Add("p_leav_year", OracleDbType.Varchar2).Value = Year;
                objCmd.Parameters.Add("p_comp_code", OracleDbType.Varchar2).Value = Comp_code;
                objCmd.Parameters.Add("p_leav_type_code", OracleDbType.Varchar2).Value = "";
                // Always pass empty as it is not needed for the desired data but to run the cursor.



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
                    leaveBalanceDataList.Add(new Balance
                    {
                        Leave_type_name = row["leav_type_name"].ToString(),
                        Leave_type_code = row["leav_type_code"].ToString(),
                        Leave_catg_code = row["leav_catg_code"].ToString(),
                        Leave_type_Short_name = row["leav_type_srtnm"].ToString(),
                        Alotted_days = row["altd_leav_days"].ToString(),
                        Taken_days = row["avld_leav_days"].ToString(),
                        Balance_days = row["blnc_leav_days"].ToString()
                    });
                }
                Oracleconnection.Close();
                Oracleconnection.Dispose();
                return leaveBalanceDataList;
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
                pack = "pkg01_hrms_lap02.prcs_leav_recommend";
            }
            else if (infoList[0].recomOrApv == "recom_Rej")
            {
                pack = "pkg01_hrms_lap02.prcs_leav_reject";
            }
            else if (infoList[0].recomOrApv == "apv")
            {
                pack = "pkg01_hrms_lap02.prcs_leav_approve";
            }
            else if (infoList[0].recomOrApv == "apv_Rej")
            {
                pack = "pkg01_hrms_lap02.prcs_leav_not_approve";
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

                            if (infoList[i].recomOrApv != "apv" && infoList[i].recomOrApv != "apv_Rej")
                            {
                                command.Parameters.Add("p_emp_code", OracleDbType.Varchar2).Value = infoList[i].UserID;
                                command.Parameters.Add("p_leav_apln_emp_code", OracleDbType.Varchar2).Value = infoList[i].Emp_code;
                            }


                            command.Parameters.Add("p_leav_apln_code", OracleDbType.Varchar2).Value = infoList[i].Leav_apln_code;
                            command.Parameters.Add("p_comp_code", OracleDbType.Varchar2).Value = infoList[i].comp_code;
                            command.Parameters.Add("p_note", OracleDbType.Varchar2).Value = infoList[i].note;
                            if (infoList[i].recomOrApv == "apv")
                            {
                                command.Parameters.Add("p_emp_code", OracleDbType.Varchar2).Value = infoList[i].Emp_code;
                                command.Parameters.Add("p_login_emp_code", OracleDbType.Varchar2).Value = infoList[i].UserID;
                                command.Parameters.Add("p_leave_taken_days", OracleDbType.Varchar2).Value = infoList[i].Leav_takn_days;
                                command.Parameters.Add("p_leav_catg_code", OracleDbType.Varchar2).Value = infoList[i].Leav_catg_code;
                                command.Parameters.Add("p_leav_year", OracleDbType.Varchar2).Value = infoList[i].year;
                                command.Parameters.Add("p_leav_type_code", OracleDbType.Varchar2).Value = infoList[i].Leav_type_code;
                            }

                            if (infoList[i].recomOrApv == "apv_Rej")
                            {
                                command.Parameters.Add("p_emp_code", OracleDbType.Varchar2).Value = infoList[i].Emp_code;
                                command.Parameters.Add("p_login_emp_code", OracleDbType.Varchar2).Value = infoList[i].UserID;
                            }

                            //command.Parameters.Add("p_entered_by", OracleType.NVarChar).Value = UserID;
                            //command.Parameters.Add("p_work_station", OracleType.NVarChar).Value = Environment.MachineName;

                            command.Parameters.Add("p_out", OracleDbType.Int32, 100).Direction = ParameterDirection.Output;
                            command.BindByName = true;
                            command.ExecuteNonQuery();
                            int returnValue = Convert.ToInt32(command.Parameters["p_out"].Value.ToString());

                            statusandmessage = dbHelper.GetStatusAndMessage("HRMF_LAP02", returnValue.ToString());
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



        public Response_data ChangeLeaveServiceData(ChangeLeave changeLeave)
        {
            string status = string.Empty, message = string.Empty;
            var statusandmessage = new Response_data();
            using (OracleConnection connection = new OracleConnection(OracleDBConnection.ConnectionStringRead()))
            {
                // create a new OracleCommand object
                using (OracleCommand command = new OracleCommand("pkg01_hrms_lap02.prcs_leav_change", connection))
                {
                    try
                    {
                        // set the command type to stored procedure
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        // add any parameters the stored procedure requires

                        command.Parameters.Add("p_emp_code", OracleDbType.Varchar2).Value = changeLeave.UserID;
                        command.Parameters.Add("p_leav_apln_emp_code", OracleDbType.Varchar2).Value = changeLeave.leav_apln_emp_code;
                        command.Parameters.Add("p_leave_type", OracleDbType.Varchar2).Value = changeLeave.leave_type_code;
                        command.Parameters.Add("p_leav_apln_code", OracleDbType.Varchar2).Value = changeLeave.leave_apln_code;
                        command.Parameters.Add("p_from_date", OracleDbType.Varchar2).Value = changeLeave.from_date;
                        command.Parameters.Add("p_to_date", OracleDbType.Varchar2).Value = changeLeave.to_date;
                        command.Parameters.Add("p_leav_takn_days", OracleDbType.Varchar2).Value = changeLeave.taken_days;
                        command.Parameters.Add("p_comp_code", OracleDbType.Varchar2).Value = changeLeave.comp_code;
                        command.Parameters.Add("p_note", OracleDbType.Varchar2).Value = changeLeave.note;

                        command.Parameters.Add("p_out", OracleDbType.Int32, 100).Direction = ParameterDirection.Output;
                        command.BindByName = true;
                        connection.Open();
                        command.ExecuteNonQuery();
                        int returnValue = Convert.ToInt32(command.Parameters["p_out"].Value.ToString());

                        statusandmessage = dbHelper.GetStatusAndMessage("HRMF_LAP02", returnValue.ToString());
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
