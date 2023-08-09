using CFRs.BLL.CONFIG;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Data;
using System.Text;

namespace CFRs.BE.Controllers.LDAP
{
    public class LDAPController : ControllerBase
    {
        [HttpGet, Authorize]
        [Route("api/LDAP/CheckAD")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public IActionResult CheckAD(string Username, string Password)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                DataTable dtConfig = ConfigBLL.Instance.GetConfigBLL($" AND CONFIG_NAME = 'LDAP'");

                if (dtConfig.Rows.Count == 0)
                    throw new Exception("Not found config LDAP.");

                string json = string.Empty;
                string URL = dtConfig.Rows[0]["CONFIG_VALUE_1"].ToString();
                string Caller = dtConfig.Rows[0]["CONFIG_VALUE_2"].ToString();
                string CallerPassword = dtConfig.Rows[0]["CONFIG_VALUE_3"].ToString();

                var options = new RestClientOptions(URL)
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest("/WSLDAP/WSLDAPUserIsExists.asmx?WSDL", Method.Post);
                request.AddHeader("Content-Type", "application/soap+xml");
                var body = @"<?xml version=""1.0"" encoding=""utf-8""?>" + "\n" +
                $@"<soap12:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap12=""http://www.w3.org/2003/05/soap-envelope"">" + "\n" +
                $@"  <soap12:Body>" + "\n" +
                $@"    <UserIsExists xmlns=""http://tempuri.org/"">" + "\n" +
                $@"      <userinfo>" + "\n" +
                $@"        <caller>{Caller}</caller>" + "\n" +
                $@"        <callerpassword>{CallerPassword}</callerpassword>" + "\n" +
                $@"        <username>{Username}</username>" + "\n" +
                $@"        <password>{Password}</password>" + "\n" +
                $@"      </userinfo>" + "\n" +
                $@"      <IsExistsUser>ossakchaip</IsExistsUser>" + "\n" +
                $@"    </UserIsExists>" + "\n" +
                $@"  </soap12:Body>" + "\n" +
                $@"</soap12:Envelope>" + "\n" +
                $@"";
                request.AddStringBody(body, DataFormat.Xml);
                RestResponse res = client.Execute(request);

                DataTable dtReturn = new DataTable();
                dtReturn.Columns.Add("USERNAME");
                dtReturn.Columns.Add("IS_LOGIN_SUCCESS");
                dtReturn.Columns.Add("REMARK");

                if (res.Content.Contains("<status>true</status>"))
                {
                    dtReturn.Rows.Add(
                          Username
                        , "Y"
                        , string.Empty
                        );
                }
                else
                {
                    dtReturn.Rows.Add(
                          Username
                        , "N"
                        , "Logon failure: unknown user name or bad password."
                        );
                }

                json = JsonConvert.SerializeObject(dtReturn, Formatting.None);
                response.Content = new StringContent(json, Encoding.UTF8, "application/json");

                return Ok(json);
            }
            catch (Exception ex)
            {
                response.Content = new StringContent(ex.Message, Encoding.UTF8, "application/json");
                return BadRequest(ex.Message);
            }
        }
    }
}