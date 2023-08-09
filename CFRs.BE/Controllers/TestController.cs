using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Text;

namespace CFRs.BE.Controllers
{
    public class TestController : ControllerBase
    {
        [HttpGet, Authorize]
        [Route("api/Test/GetTest")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public IActionResult GetTest(string Text)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                string json = string.Empty;

                DataTable dtReturn = new DataTable();

                //dtReturn = GI_BLL.Instance.GetDeliveryConfirmBLL(Text);
                dtReturn.Columns.Add("COL_1");

                dtReturn.Rows.Add("ABC");

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
