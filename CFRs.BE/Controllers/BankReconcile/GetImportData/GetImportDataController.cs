using CFRs.BE.Helper;
using CFRs.BLL.BANK_RECONCILE;
using GemBox.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace CFRs.BE.Controllers.BankReconcile.GetImportData
{
    public class GetImportDataController : ControllerBase
    {
        [HttpGet]
        [Route("api/BankStatement/GetImportData/Get")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public IActionResult Get(string SystemCode, string BankShortName
            , string FromDate, string ToDate
            , string IsExport)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            string json = string.Empty;

            try
            {
                LogHelper.WriteLog("CFRs", "INF", "Call.. api/BankStatement/GetImportData/Get");

                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                string Condition = string.Empty;
                string ConditionDateEBAO_LS = string.Empty;
                string ConditionDateEBAO_GLS = string.Empty;
                string ConditionDateDMS = string.Empty;
                string ConditionDateSmartRP = string.Empty;

                if (!string.IsNullOrEmpty(SystemCode))
                    Condition += $" AND SYSTEM_CODE = '{SystemCode}'";

                if (!string.IsNullOrEmpty(BankShortName))
                    Condition += $" AND BANK_SHORT_NAME = '{BankShortName}'";

                if (!string.IsNullOrEmpty(FromDate))
                {
                    Condition += $" AND COL_DATE_WHERE BETWEEN CONVERT(DATE, '{FromDate}', 103) AND CONVERT(DATE, '{ToDate}', 103)";
                }

                DataTable dtReturn = SourceImport_BLL.Instance.GetImportBLL(Condition);

                if (string.Equals(IsExport, "0"))
                {
                    json = JsonConvert.SerializeObject(dtReturn, Formatting.None);
                    response.Content = new StringContent(json, Encoding.UTF8, "application/json");
                }
                else
                {
                    dtReturn.Columns.Remove("DETAIL_ID");
                    dtReturn.Columns.Remove("HEADER_ID");
                    dtReturn.Columns.Remove("COL_DATE_WHERE");

                    ExcelFile workbook = ExcelFile.Load(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\FileFormat\\ImportData.xlsx");
                    ExcelWorksheet worksheet = workbook.Worksheets["Sheet1"];

                    int Row = 1;
                    int Column = 0;

                    for (int i = 0; i < dtReturn.Rows.Count; i++)
                    {
                        Column = 0;

                        for (int j = 0; j < dtReturn.Columns.Count; j++)
                        {
                            if (i == 0)
                            {//Write Header Text
                                worksheet.Cells[i, j].Value = dtReturn.Columns[j].ColumnName.ToUpper();
                            }

                            if (string.Equals(dtReturn.Columns[j].ColumnName, "DUE_YEAR"))
                            {
                                worksheet.Cells[Row, Column].Value = int.Parse(dtReturn.Rows[i][j].ToString());
                            }
                            else if (string.Equals(dtReturn.Columns[j].ColumnName, "PAYAMT"))
                            {
                                worksheet.Cells[Row, Column].Value = decimal.Parse(dtReturn.Rows[i][j].ToString());
                            }
                            else
                                worksheet.Cells[Row, Column].Value = dtReturn.Rows[i][j].ToString();

                            if (i > 0)
                            {//Copy Format
                                worksheet.Cells[Row, Column].Style = worksheet.Cells[Row - 1, Column].Style;
                            }

                            Column++;
                        }

                        Row++;
                    }

                    int columnCount = worksheet.CalculateMaxUsedColumns();
                    for (int i = 0; i < columnCount; i++)
                        worksheet.Columns[i].AutoFit(1, worksheet.Rows[0], worksheet.Rows[worksheet.Rows.Count - 1]);

                    string Path = this.CheckPath();

                    string DateNow = DateTime.Now.ToString("yyyyMMdd_HHmmss", new CultureInfo("th-TH"));
                    string FileName = $"ImportData_" + DateNow + ".xlsx";

                    var location = new Uri($"{Request.Scheme}://{Request.Host}");
                    var url = location.AbsoluteUri;

                    workbook.Save(Path + "\\" + FileName);

                    dtReturn = new DataTable();
                    dtReturn.Columns.Add("EXPORT_PATH");

                    //dtReturn.Rows.Add(Path + "\\" + FileName);
                    dtReturn.Rows.Add($"{url}Export/{FileName}");

                    json = JsonConvert.SerializeObject(dtReturn, Formatting.None);
                    response.Content = new StringContent(json, Encoding.UTF8, "application/json");
                }

                return Ok(json);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("CFRs", "ERR", "Call.. api/BankStatement/GetImportData/Get exception : " + ex.Message);

                response.Content = new StringContent(ex.Message, Encoding.UTF8, "application/json");
                return BadRequest(ex.Message);
            }
            finally
            {
                LogHelper.WriteLog("CFRs", "INF", "Call Ended.. api/BankStatement/GetImportData/Get");
            }
        }

        [HttpGet]
        [Route("api/BankStatement/GetImportData/SAPGet")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public IActionResult SAPGet(string SystemCode
            , string FromDate, string ToDate
            , string IsExport)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            string json = string.Empty;

            try
            {
                LogHelper.WriteLog("CFRs", "INF", "Call.. api/BankStatement/GetImportData/SAPGet");

                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                string Condition = string.Empty;
                string ConditionSAP = string.Empty;

                if (!string.IsNullOrEmpty(SystemCode))
                    Condition += $" AND SYSTEM_CODE = '{SystemCode}'";

                //if (!string.IsNullOrEmpty(BankCode))
                //    Condition += $" AND BANK_CODE = '{BankCode}'";

                if (!string.IsNullOrEmpty(FromDate))
                {
                    Condition += $" AND COL_DATE_WHERE BETWEEN CONVERT(DATE, '{FromDate}', 103) AND CONVERT(DATE, '{ToDate}', 103)";
                }

                DataTable dtReturn = SourceImport_BLL.Instance.GetImportSAP_BLL(Condition);

                if (string.Equals(IsExport, "0"))
                {
                    json = JsonConvert.SerializeObject(dtReturn, Formatting.None);
                    response.Content = new StringContent(json, Encoding.UTF8, "application/json");
                }
                else
                {
                    dtReturn.Columns.Remove("DETAIL_ID");
                    dtReturn.Columns.Remove("HEADER_ID");
                    dtReturn.Columns.Remove("COL_DATE_WHERE");

                    ExcelFile workbook = ExcelFile.Load(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\FileFormat\\ImportDataSAP.xlsx");
                    ExcelWorksheet worksheet = workbook.Worksheets["Sheet1"];

                    int Row = 1;
                    int Column = 0;

                    for (int i = 0; i < dtReturn.Rows.Count; i++)
                    {
                        Column = 0;

                        for (int j = 0; j < dtReturn.Columns.Count; j++)
                        {
                            if (i == 0)
                            {//Write Header Text
                                worksheet.Cells[i, j].Value = dtReturn.Columns[j].ColumnName.ToUpper();
                            }

                            if (string.Equals(dtReturn.Columns[j].ColumnName, "DUE_YEAR"))
                            {
                                worksheet.Cells[Row, Column].Value = int.Parse(dtReturn.Rows[i][j].ToString());
                            }
                            else if (string.Equals(dtReturn.Columns[j].ColumnName, "PAYAMT"))
                            {
                                worksheet.Cells[Row, Column].Value = decimal.Parse(dtReturn.Rows[i][j].ToString());
                            }
                            else
                                worksheet.Cells[Row, Column].Value = dtReturn.Rows[i][j].ToString();

                            if (i > 0)
                            {//Copy Format
                                worksheet.Cells[Row, Column].Style = worksheet.Cells[Row - 1, Column].Style;
                            }

                            Column++;
                        }

                        Row++;
                    }

                    int columnCount = worksheet.CalculateMaxUsedColumns();
                    for (int i = 0; i < columnCount; i++)
                        worksheet.Columns[i].AutoFit(1, worksheet.Rows[0], worksheet.Rows[worksheet.Rows.Count - 1]);

                    string Path = this.CheckPath();

                    string DateNow = DateTime.Now.ToString("yyyyMMdd_HHmmss", new CultureInfo("th-TH"));
                    string FileName = $"ImportDataSAP_" + DateNow + ".xlsx";

                    var location = new Uri($"{Request.Scheme}://{Request.Host}");
                    var url = location.AbsoluteUri;

                    workbook.Save(Path + "\\" + FileName);

                    dtReturn = new DataTable();
                    dtReturn.Columns.Add("EXPORT_PATH");

                    //dtReturn.Rows.Add(Path + "\\" + FileName);
                    dtReturn.Rows.Add($"{url}Export/{FileName}");

                    json = JsonConvert.SerializeObject(dtReturn, Formatting.None);
                    response.Content = new StringContent(json, Encoding.UTF8, "application/json");
                }

                return Ok(json);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("CFRs", "ERR", "Call.. api/BankStatement/GetImportData/SAPGet exception : " + ex.Message);

                response.Content = new StringContent(ex.Message, Encoding.UTF8, "application/json");
                return BadRequest(ex.Message);
            }
            finally
            {
                LogHelper.WriteLog("CFRs", "INF", "Call Ended.. api/BankStatement/GetImportData/SAPGet");
            }
        }

        private string CheckPath()
        {
            try
            {
                string PathExport = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\" + "Export";
                if (!Directory.Exists(PathExport))
                    Directory.CreateDirectory(PathExport);

                return PathExport;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}