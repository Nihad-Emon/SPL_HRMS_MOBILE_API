using HRMS_API.DataServices;
using HRMS_API.Models;
using HRMS_API.Models.LeaveApplication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace HRMS_API.Controllers.Security
{
    [Route("api/Login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        //http://localhost:9572/api/Login
        private IConfiguration _configuration;
        private readonly OracleDBConnection OracleDBConnection;
        private readonly DBHelper dbHelper;
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
            OracleDBConnection = new OracleDBConnection(configuration);
            dbHelper = new DBHelper(configuration);
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Post(LoginRequest loginRequest)
        {

            string OracleMConnection = _configuration.GetConnectionString("databseConnection");
            OracleConnection Oracleconection = new OracleConnection(OracleMConnection);
            var empInfo = new List<EmployeeInfo>();
            try
            {
                Oracleconection.Open();
                SharedServices sharedServices = new SharedServices();

                // Login Logic
                loginRequest.user = loginRequest.user.ToUpper();
                string userName = string.Empty, passWord = string.Empty, RoleID = string.Empty, RoleTitle = string.Empty, UserID = string.Empty, emp_name = string.Empty, picture = string.Empty, status = string.Empty;
                string Query = "SELECT USERID,USERNAME,EMP_NAME,USERPASSWORD, APV_STATUS, rcmnd_status, emp_id, emp_designation, COMP_NAME, ROLEID, EMP_PICTURE, ROLETITLE FROM SEC_USER where USERNAME=" + "'" + loginRequest.user + "'" + " and " + "USERPASSWORD='" + loginRequest.pass + "'";
                OracleCommand Oraclecmd = new OracleCommand(Query, Oracleconection);

                OracleDataAdapter OracleDa = new OracleDataAdapter(Oraclecmd);
                DataTable OracleDt = new DataTable();
                OracleDa.Fill(OracleDt);
                if (OracleDt.Rows.Count > 0)
                {
                    for (int i = 0; i < OracleDt.Rows.Count; i++)
                    {

                        EmployeeInfo employeeInfo = new EmployeeInfo();

                        employeeInfo.User_id = OracleDt.Rows[0]["USERID"].ToString();
                        employeeInfo.User_name = OracleDt.Rows[0]["USERNAME"].ToString();
                        employeeInfo.User_pass = OracleDt.Rows[0]["USERPASSWORD"].ToString();
                        employeeInfo.Emp_name = OracleDt.Rows[0]["EMP_NAME"].ToString();
                        employeeInfo.Apv_Power = OracleDt.Rows[0]["APV_STATUS"].ToString();
                        employeeInfo.Recom_Power = OracleDt.Rows[0]["rcmnd_status"].ToString();
                        employeeInfo.Emp_id = OracleDt.Rows[0]["emp_id"].ToString();
                        employeeInfo.Designation = OracleDt.Rows[0]["emp_designation"].ToString();
                        employeeInfo.Company = OracleDt.Rows[0]["COMP_NAME"].ToString();

                        if (OracleDt.Rows[i]["EMP_PICTURE"] != DBNull.Value)
                        {
                            // Convert the byte array to a Base64 string
                            byte[] imageData = (byte[])OracleDt.Rows[i]["EMP_PICTURE"];
                            byte[] compressedImageData = CompressImage(imageData);
                            employeeInfo.picture = Convert.ToBase64String(compressedImageData);
                        }
                        else
                        {
                            // Handle DBNull case if needed
                        }

                        // Token Generation 
                        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                        var Sectoken = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                            _configuration["Jwt:Issuer"],
                            null,
                            expires: DateTime.Now.AddMinutes(120),
                            signingCredentials: credentials
                            );

                        employeeInfo.token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

                        var empInfoDetails = new List<EmpInfoDetails>();
                        empInfoDetails = GetEmployeeInfoServiceData(employeeInfo.User_id);
                        if (empInfoDetails[0].statusFlag == "1")
                        {
                            employeeInfo.Leav_apln_status = "SFR";
                            employeeInfo.Leav_rcmd_stus = "NA";
                        }
                        else if (empInfoDetails[0].statusFlag == "0")
                        {
                            employeeInfo.Leav_apln_status = "SFA";
                            employeeInfo.Leav_rcmd_stus = "RC";
                        }
                        else
                        {

                        }
                        employeeInfo.recom_emp_code = empInfoDetails[0].recom_emp_code;
                        employeeInfo.recom_emp_name = empInfoDetails[0].recom_emp_name;
                        employeeInfo.apv_emp_code = empInfoDetails[0].apv_emp_code;
                        employeeInfo.apv_emp_name = empInfoDetails[0].apv_emp_name;
                        empInfo.Add(employeeInfo);
                    }
                }
                else
                {
                    return BadRequest("Invalid username or password");
                }

                return Ok(empInfo);
            }
            catch (Exception ex)
            {
                // Handle exceptions if needed
                return StatusCode(500, "Internal Server Error");
            }
            finally
            {
                // Make sure to close the connection
                Oracleconection.Close();
            }

        }

        static byte[] CompressImage(byte[] imageData)
        {
            using (MemoryStream originalStream = new MemoryStream(imageData))
            using (MemoryStream compressedStream = new MemoryStream())
            {
                using (System.Drawing.Image originalImage = System.Drawing.Image.FromStream(originalStream))
                {
                    // You can adjust the quality parameter as needed (0.1 to 1.0)
                    originalImage.Save(compressedStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                }

                return compressedStream.ToArray();
            }
        }


        public List<EmpInfoDetails> GetEmployeeInfoServiceData(string UserID)
        {
            OracleConnection Oracleconnection = new OracleConnection(OracleDBConnection.ConnectionStringRead());

            var empInfoDetails = new List<EmpInfoDetails>();

            try
            {
                OracleCommand objCmd = new OracleCommand("pkg01_hrms_lap01.f_emp_display ", Oracleconnection);
                DataTable dt = new DataTable();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("p_emp_code", OracleDbType.Varchar2).Value = UserID;



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
                    empInfoDetails.Add(new EmpInfoDetails
                    {
                        statusFlag = row["rcmd_layer"].ToString(),
                        recom_emp_code = row["leav_rcmnd_code"].ToString(),
                        recom_emp_name = row["leav_rcmnd_name"].ToString(),
                        apv_emp_code = row["leav_aprv_code"].ToString(),
                        apv_emp_name = row["leav_aprv_name"].ToString()
                    });

                }


                Oracleconnection.Close();
                Oracleconnection.Dispose();
                return empInfoDetails;
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


        // ************** The bellow code is not related to this project. For R&D ***********************
        // The bellow code is for test purpose,
        // This is how we cam write query in web config/ appsettings.json and execute it from service.

        //[HttpGet]
        //[Route("loginxyz")]
        //public IActionResult Get(string UserID)
        //{
        //    string OracleMConnection = _configuration.GetConnectionString("databseConnection");
        //    OracleConnection Oracleconection = new OracleConnection(OracleMConnection);
        //    var empInfo = new List<Employee>();
        //    try
        //    {
        //        Oracleconection.Open();
        //        SharedServices sharedServices = new SharedServices();
        //        // Login Logic
        //        string query = _configuration["Queries:GetEmployees"];


        //        OracleCommand Oraclecmd = new OracleCommand(query, Oracleconection);

        //        OracleDataAdapter OracleDa = new OracleDataAdapter(Oraclecmd);
        //        DataTable OracleDt = new DataTable();
        //        OracleDa.Fill(OracleDt);
        //        if (OracleDt.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < OracleDt.Rows.Count; i++)
        //            {

        //                Employee employeeInfo = new Employee();

        //                employeeInfo.User_id = OracleDt.Rows[0]["USERID"].ToString();
        //                employeeInfo.Emp_name = OracleDt.Rows[0]["EMP_NAME"].ToString();                     

        //                empInfo.Add(employeeInfo);
        //            }
        //        }
        //        else
        //        {
        //            return BadRequest("Invalid Entry");
        //        }

        //        return Ok(empInfo);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exceptions if needed
        //        return StatusCode(500, "Internal Server Error");
        //    }
        //    finally
        //    {
        //        // Make sure to close the connection
        //        Oracleconection.Close();
        //    }
        //}

        [HttpGet]
        [Route("loginxyz2")]
        public IActionResult Get(string User, string pass)
        {
            string OracleMConnection = _configuration.GetConnectionString("databseConnection");
            //OracleConnection Oracleconection = new OracleConnection(OracleMConnection);
            string query = _configuration["Queries:GetEmployees"];
            var empInfo = new List<Employee>();

            using (var connection = new OracleConnection(OracleMConnection))
            {
                connection.Open();

                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(":1", OracleDbType.NVarchar2).Value = User;
                    command.Parameters.Add(":2", OracleDbType.NVarchar2).Value = pass;

                    using (var reader = command.ExecuteReader())
                    {
                        OracleDataAdapter OracleDa = new OracleDataAdapter(command);
                        DataTable OracleDt = new DataTable();
                        OracleDa.Fill(OracleDt);
                        if (OracleDt.Rows.Count > 0)
                        {
                            for (int i = 0; i < OracleDt.Rows.Count; i++)
                            {

                                Employee employeeInfo = new Employee();

                                employeeInfo.User_id = OracleDt.Rows[0]["USERID"].ToString();
                                employeeInfo.Emp_name = OracleDt.Rows[0]["EMP_NAME"].ToString();
                                employeeInfo.Apv_Power = OracleDt.Rows[0]["APV_STATUS"].ToString();
                                employeeInfo.Recom_Power = OracleDt.Rows[0]["rcmnd_status"].ToString();

                                empInfo.Add(employeeInfo);
                            }
                        }
                        else
                        {
                            return BadRequest("Invalid Entry");
                        }

                        return Ok(empInfo);
                    }
                }
            }
        }


        public class Employee
        {
            public string? User_id { get; set; }
            public string? Emp_name { get; set; }
            public string? Apv_Power { get; set; }
            public string? Recom_Power { get; set; }
            
        }
    }
}
