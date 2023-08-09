using BankReconcile.BLL.EBAO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Text;

namespace CFRs.BE.EBAO
{
    public class EBAO_DataController : ControllerBase
    {
        [HttpGet, Authorize]
        [Route("api/EBAO/GetTest")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public IActionResult GetTest()
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                string json = string.Empty;

                DataTable dtReturn = new DataTable();

                dtReturn = EBAO_Data_BLL.Instance.GetTestBLL();

                json = JsonConvert.SerializeObject(dtReturn, Formatting.None);
                response.Content = new StringContent(json, Encoding.UTF8, "application/json");

                return Ok(json);
            }
            catch (Exception ex)
            {
                response.Content = new StringContent(ex.Message, Encoding.UTF8, "application/json");
                return BadRequest();
            }
        }
    }
}