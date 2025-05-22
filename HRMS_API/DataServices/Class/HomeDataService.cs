using HRMS_API.DataServices.Interface;
using HRMS_API.Models.Home;
using HRMS_API.Services.Interface;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Net.NetworkInformation;

namespace HRMS_API.DataServices.Class
{
    public class HomeDataService : IHomeDataService
    {
        readonly IConfiguration _configuration;
        private readonly OracleDBConnection OracleDBConnection;

        public HomeDataService(IConfiguration configuration)
        {
            _configuration = configuration;
            OracleDBConnection = new OracleDBConnection(configuration);
        }


        public List<LeaveYear> GetLeavelYearServiceData(string UserID)
        {
            OracleConnection Oracleconnection = new OracleConnection(OracleDBConnection.ConnectionStringRead());

            var leaveYearList = new List<LeaveYear>();

            try
            {
                OracleCommand objCmd = new OracleCommand("pkg01_hrms_lap01.f_year_list", Oracleconnection);
                DataTable dt = new DataTable();
                objCmd.CommandType = CommandType.StoredProcedure;

                objCmd.Parameters.Add("p_emp_code", OracleDbType.Varchar2).Value = UserID;
                objCmd.Parameters["p_emp_code"].Direction = ParameterDirection.Input;

                objCmd.Parameters.Add("return_value", OracleDbType.RefCursor).Direction = ParameterDirection.ReturnValue;
                objCmd.BindByName = true;  // it is used for define the name of columns { Serializer and Deserializer issue}  // Tin din Vogaicghe!

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
                    leaveYearList.Add(new LeaveYear { Year = row["leav_year"].ToString() });
                }

                Oracleconnection.Close();
                Oracleconnection.Dispose();
                return leaveYearList;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Oracleconnection.Close();
                Oracleconnection.Dispose();
            }
        }





        public List<Balance> GetBalanceServiceData(string UserID, string Year)
        {
            OracleConnection Oracleconnection = new OracleConnection(OracleDBConnection.ConnectionStringRead());

            var balance = new List<Balance>();

            try
            {
                OracleCommand objCmd = new OracleCommand("pkg01_hrms_lap01.f_leav_balnc_display", Oracleconnection);
                DataTable dt = new DataTable();
                objCmd.CommandType = CommandType.StoredProcedure;

                objCmd.Parameters.Add("p_emp_code", OracleDbType.Varchar2).Value = UserID;
                objCmd.Parameters["p_emp_code"].Direction = ParameterDirection.Input;
                objCmd.Parameters.Add("p_leav_year", OracleDbType.Varchar2).Value = Year;
                objCmd.Parameters["p_leav_year"].Direction = ParameterDirection.Input;
                objCmd.Parameters.Add("p_leav_type_code", OracleDbType.Varchar2).Value = "";   // Always empty for seeing overall Balance
                objCmd.Parameters["p_leav_type_code"].Direction = ParameterDirection.Input;

                objCmd.Parameters.Add("return_value", OracleDbType.RefCursor).Direction = ParameterDirection.ReturnValue;
                objCmd.BindByName = true;  // it is used for define the name of columns { Serializer and Deserializer issue}  // Tin din Vogaicghe!

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
                    balance.Add(new Balance
                    {
                        Leave_type_name = row["leav_type_name"].ToString(),
                        Leave_type_Short_name = row["leav_type_srtnm"].ToString(),
                        Leave_type_code = row["leav_type_code"].ToString(),
                        Alotted_days = row["altd_leav_days"].ToString(),
                        Taken_days = row["avld_leav_days"].ToString(),
                        Balance_days = row["blnc_leav_days"].ToString(),
                        Leave_catg_code = row["leav_catg_code"].ToString()
                    });
                }

                Oracleconnection.Close();
                Oracleconnection.Dispose();
                return balance;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Oracleconnection.Close();
                Oracleconnection.Dispose();
            }
        }

        public List<Total> GetLeaveCountServiceData(string Year, string ToBrowse, string UserID)
        {
            OracleConnection Oracleconnection = new OracleConnection(OracleDBConnection.ConnectionStringRead());

            var totalList = new List<Total>();
            string pack = "pkg03_hrms_DBM03.prcs_leave_summary_rcmnd_dis";
            Oracleconnection.Open();

            try
            {
                for (int j = 0; j < 4; j++)
                {

                    if (j == 0)
                    {
                        pack = "pkg03_hrms_DBM03.prcs_leave_summary_rcmnd_dis"; // Leave                     
                    }
                    else if (j == 1)
                    {
                        pack = "pkg03_hrms_DBM03.PRCS_OFD_SUMMARY_RCMND_DIS"; // out off              
                    }
                    else if (j == 2)
                    {
                        pack = "pkg03_hrms_DBM03.PRCS_EO_SUMMARY_RCMND_DIS";  // Early Out
                    }
                    else if (j == 3)
                    {
                        pack = "pkg03_hrms_DBM03.PRCS_LT_SUMMARY_RCMND_DIS";  // Late
                    }
                    else
                    {
                        pack = "No_pack";
                    }

                    OracleCommand objCmd = new OracleCommand(pack, Oracleconnection);
                    DataTable dt = new DataTable();
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("p_emp_code", OracleDbType.Varchar2).Value = UserID;
                    objCmd.Parameters["p_emp_code"].Direction = ParameterDirection.Input;
                    if (pack == "pkg03_hrms_DBM03.prcs_leave_summary_rcmnd_dis")
                    {
                        objCmd.Parameters.Add("p_leave_year", OracleDbType.Varchar2).Value = Year;
                        objCmd.Parameters["p_leave_year"].Direction = ParameterDirection.Input;
                    }
                    else
                    {
                        objCmd.Parameters.Add("P_YEAR", OracleDbType.Varchar2).Value = Year;
                        objCmd.Parameters["P_YEAR"].Direction = ParameterDirection.Input;
                    }

                    objCmd.Parameters.Add("p_chck_flg", OracleDbType.Varchar2).Value = ToBrowse;
                    objCmd.Parameters["p_chck_flg"].Direction = ParameterDirection.Input;



                    objCmd.Parameters.Add("return_value", OracleDbType.RefCursor).Direction = ParameterDirection.ReturnValue;
                    objCmd.BindByName = true;



                    using (OracleDataReader odr = objCmd.ExecuteReader())
                    {
                        if (odr.HasRows)
                        {
                            dt.Load(odr);
                        }
                    }

                    foreach (DataRow row in dt.Rows)
                    {
                        totalList.Add(new Total
                        {
                            total = Convert.ToInt32(row["TOTAL"].ToString()),
                            pending = Convert.ToInt32(row["TOT_PEND_RCMD"].ToString()),
                            approval = Convert.ToInt32(row["TOT_PEND_APRV"].ToString())
                        });
                    }
                }

                Oracleconnection.Close();
                Oracleconnection.Dispose();
                return totalList;
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



        public int GetPendingAplnCountServiceData(string UserID, string Year)
        {
            OracleConnection Oracleconnection = new OracleConnection(OracleDBConnection.ConnectionStringRead());

            //var pndLeaveApplicationData = new List<PndLeaveApplication>();
            int pending = 0;

            try
            {
                OracleCommand objCmd = new OracleCommand("pkg01_hrms_lap01.F_LEAV_PEND_LIST_DISPLAY ", Oracleconnection);
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
                    pending++;
                }

                Oracleconnection.Close();
                Oracleconnection.Dispose();
                return pending;
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



        public List<PndLeaveApplication> GetPndLeaveApplicationListServiceData(string UserID, string Year)
        {
            OracleConnection Oracleconnection = new OracleConnection(OracleDBConnection.ConnectionStringRead());

            var pndLeaveApplicationData = new List<PndLeaveApplication>();

            try
            {
                OracleCommand objCmd = new OracleCommand("pkg01_hrms_lap01.F_LEAV_PEND_LIST_DISPLAY ", Oracleconnection);
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
                    pndLeaveApplicationData.Add(new PndLeaveApplication
                    {
                        SL = Convert.ToInt16(row["LEAV_APLN_SLNO"].ToString()),
                        Type = row["LEAV_TYPE_NAME"].ToString(),
                        Application_date = row["LEAV_APLN_DATE"].ToString(),
                        From_date = row["LEAV_FROM_DATE"].ToString(),
                        To_date = row["LEAV_TO_DATE"].ToString(),
                        Days = row["LEAV_TAKN_DAYS"].ToString(),
                        Leav_takn_caus = row["leav_takn_caus"].ToString(),
                        Leav_days_stay = row["leav_days_stay"].ToString(),
                        Rcmd_emp_name = row["rcmd_emp_name"].ToString(),
                        Aprv_emp_name = row["aprv_emp_name"].ToString(),
                        ColorStatus = row["leav_apln_status"].ToString(),
                        Status = row["leav_apln_status_details"].ToString()
                    });
                }

                Oracleconnection.Close();
                Oracleconnection.Dispose();
                return pndLeaveApplicationData;
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

        public List<LeaveHistory> GetLeaveHistoryServiceData(string UserID, string Year)
        {
            OracleConnection Oracleconnection = new OracleConnection(OracleDBConnection.ConnectionStringRead());

            var leaveHistorylist = new List<LeaveHistory>();

            try
            {
                OracleCommand objCmd = new OracleCommand("pkg01_hrms_lap01.f_leav_apln_histy_display ", Oracleconnection);
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
                    leaveHistorylist.Add(new LeaveHistory
                    {
                        From_date = row["leav_from_date"].ToString(),
                        To_date = row["leav_to_date"].ToString(),
                        Leave_days = row["leav_takn_days"].ToString(),
                        Leave_detail = row["LEAVE_DETAIL"].ToString(),
                        colorStatus = row["leav_pstg_stus"].ToString(),
                    });
                }

                Oracleconnection.Close();
                Oracleconnection.Dispose();
                return leaveHistorylist;
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


        public List<LeaveHistory> GetLeaveHistoryServiceForOthersData(string UserID, string Year, string comp_code)
        {
            OracleConnection Oracleconnection = new OracleConnection(OracleDBConnection.ConnectionStringRead());

            var leaveHistorylist = new List<LeaveHistory>();

            try
            {
                OracleCommand objCmd = new OracleCommand("pkg01_hrms_lap02.f_leav_apln_histy_dis ", Oracleconnection);
                DataTable dt = new DataTable();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("p_emp_code", OracleDbType.Varchar2).Value = UserID;
                objCmd.Parameters["p_emp_code"].Direction = ParameterDirection.Input;
                objCmd.Parameters.Add("p_leav_year", OracleDbType.Varchar2).Value = Year;
                objCmd.Parameters["p_leav_year"].Direction = ParameterDirection.Input;
                objCmd.Parameters.Add("p_comp_code", OracleDbType.Varchar2).Value = comp_code;
                objCmd.Parameters["p_comp_code"].Direction = ParameterDirection.Input;


                objCmd.Parameters.Add("rc_leav_apln_histy_display", OracleDbType.RefCursor).Direction = ParameterDirection.ReturnValue;
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
                    leaveHistorylist.Add(new LeaveHistory
                    {
                        From_date = row["leav_from_date"].ToString(),
                        To_date = row["leav_to_date"].ToString(),
                        Leave_days = row["leav_takn_days"].ToString(),
                        Leave_detail = row["LEAVE_DETAIL"].ToString(),
                        colorStatus = row["leav_pstg_stus"].ToString(),
                    });
                }

                Oracleconnection.Close();
                Oracleconnection.Dispose();
                return leaveHistorylist;
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

        public List<Absent> GetAbsentListServiceData(string UserID, string Year)
        {
            OracleConnection Oracleconnection = new OracleConnection(OracleDBConnection.ConnectionStringRead());

            var absentList = new List<Absent>();

            try
            {
                OracleCommand objCmd = new OracleCommand("pkg01_hrms_lap01.f_emp_absnt_days ", Oracleconnection);
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
                    absentList.Add(new Absent
                    {
                        SL = Convert.ToInt16(row["sl"].ToString()),
                        Month = row["mon"].ToString(),
                        Date = row["atdn_date"].ToString(),
                        Day = row["dy"].ToString()
                    });
                }

                Oracleconnection.Close();
                Oracleconnection.Dispose();
                return absentList;
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

        public List<IpPhone> GetIpPhoneListServiceData(string UserID)
        {
            OracleConnection Oracleconnection = new OracleConnection(OracleDBConnection.ConnectionStringRead());

            var ipPhoneList = new List<IpPhone>();

            try
            {
                OracleCommand objCmd = new OracleCommand("pkg_hrms_list01.f_ipphone_list", Oracleconnection);
                DataTable dt = new DataTable();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("p_emp_code", OracleDbType.Varchar2).Value = UserID;

                objCmd.Parameters.Add("return_value", OracleDbType.RefCursor).Direction = ParameterDirection.ReturnValue;
                objCmd.BindByName = true;

                Oracleconnection.Open();
                OracleDataReader odr = objCmd.ExecuteReader();

                if (odr.HasRows)
                {
                    dt.Load(odr);
                }

                foreach (DataRow row in dt.Rows)
                {
                    ipPhoneList.Add(new IpPhone
                    {
                        Emp_Name = row["emp_name"].ToString(),
                        IpNumber = row["ip_number"].ToString(),
                        Designation = row["Designation"].ToString(),
                        Department = row["Department"].ToString(),
                        WorkStation = row["Work_Station"].ToString()
                    });
                }
                Oracleconnection.Close();
                Oracleconnection.Dispose();
                return ipPhoneList;
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

        public List<Holiday> GetHolidayListServiceData(string UserID)
        {
            OracleConnection Oracleconnection = new OracleConnection(OracleDBConnection.ConnectionStringRead());

            var holidayList = new List<Holiday>();

            try
            {
                OracleCommand objCmd = new OracleCommand("pkg_hrms_list01.f_holiday_list", Oracleconnection);
                DataTable dt = new DataTable();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("p_emp_code", OracleDbType.Varchar2).Value = UserID;

                objCmd.Parameters.Add("return_value", OracleDbType.RefCursor).Direction = ParameterDirection.ReturnValue;
                objCmd.BindByName = true;

                Oracleconnection.Open();
                OracleDataReader odr = objCmd.ExecuteReader();

                if (odr.HasRows)
                {
                    dt.Load(odr);
                }
                foreach (DataRow row in dt.Rows)
                {
                    holidayList.Add(new Holiday
                    {
                        date = row["ddate"].ToString(),
                        day = row["day1"].ToString(),
                        holiday = row["HLDY_NAME"].ToString()
                    });
                }
                Oracleconnection.Close();
                Oracleconnection.Dispose();
                return holidayList;
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


        //////////////
    }

}
