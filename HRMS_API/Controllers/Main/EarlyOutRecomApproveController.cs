using HRMS_API.Models.EarlyOutRecomApv;
using HRMS_API.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static HRMS_API.DataServices.DBHelper;

namespace HRMS_API.Controllers.Main
{
    [Route("api/EarlyOutRecomApv")]
    [ApiController]
    public class EarlyOutRecomApproveController : ControllerBase
    {
        private IEarlyOutRecomApvService _earlyOutRecomApvService;

        public EarlyOutRecomApproveController(IEarlyOutRecomApvService earlyOutRecomApvService)
        {
            _earlyOutRecomApvService = earlyOutRecomApvService;
        }


        [HttpGet]
        [Route("recom_apv_list")]
        public IActionResult Post(string year, string toBrowse, string recomOrApv, string UserID)
        {
            try
            {
                var recomApvList = new List<RecomApv>();
                _earlyOutRecomApvService.GetRecomApvListService(year, toBrowse, recomOrApv, UserID, out recomApvList);

                string msg = "Data loaded";
                if (recomApvList.Count < 1)
                {
                    msg = "No data was found in the Database";
                }

                return Ok(new { RecomApvList = recomApvList, Message = msg });
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }




        [HttpGet]
        [Route("recom_apv_table")]
        public IActionResult Post(string year, string comp_code, string toBrowse, string recomOrApv, string UserID)
        {
            try
            {
                var recomApvTableDataList = new List<RecomApvData>();
                _earlyOutRecomApvService.GetRecomApvTableService(year, comp_code, toBrowse, recomOrApv, UserID, out recomApvTableDataList);

                string msg = "Data loaded";
                if (recomApvTableDataList.Count < 1)
                {
                    msg = "No data was found in the Database";
                }

                return Ok(new { RecomApvTableDataList = recomApvTableDataList, Message = msg });
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }



        [HttpPost]
        [Route("submit_eo_recom_apv")]
        public IActionResult Post([FromBody] List<RecomApvData> infoList)
        {
            try
            {
                var status = new Response_data();
                status = _earlyOutRecomApvService.SubmitRecomApvService(infoList);

                return Ok(new { ResponseCode = status.status, Message = status.message, MsgType = status.msg_type });
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }
    }
}
