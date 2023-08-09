using CFRs.BE.Helper;
using CFRs.ENT.BankStatement;
using Ionic.Zip;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using System.Data;
using System.Text;

namespace CFRs.BE.Controllers.BankReconcile.BankStatement
{
    public class ImportFileController : ControllerBase
    {
        [HttpPost, Authorize]
        [Route("api/BankStatement/ImportFile/Import")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public IActionResult Import(int MonthID
            , int Year
            , string BankShortName
            , int UserID
            , [FromBody] BankStatementImportEnt[] bankStatementImportEnt)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            string json = string.Empty;

            try
            {
                LogHelper.WriteLog("INF", "Call.. api/BankStatement/ImportFile/Import");

                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                if (!string.Equals(BankShortName.ToUpper(), "BAAC")
                    && !string.Equals(BankShortName.ToUpper(), "KBANK")
                    && !string.Equals(BankShortName.ToUpper(), "KTB"))
                {
                    LogHelper.WriteLog("ERR", $"Bank {BankShortName} not found logic import file.");
                    throw new Exception($"Bank {BankShortName} not found logic import file.");
                }

                DataTable dtReturn = new DataTable();
                dtReturn.Columns.Add("ZIP_FILE_NAME");
                dtReturn.Columns.Add("FILE_PATH");
                dtReturn.Columns.Add("FILE_NAME");
                dtReturn.Columns.Add("RESULT");
                dtReturn.Columns.Add("MESSAGE");

                //Check Case Unzip (.zip, .rar)
                for (int k = 0; k < bankStatementImportEnt.Length; k++)
                {
                    string FilePath = bankStatementImportEnt[k].FilePath;
                    string FileName = Path.GetFileName(FilePath);
                    string FileExt = Path.GetExtension(FilePath);

                    if (string.Equals(FileExt.ToLower(), ".zip"))
                    {
                        LogHelper.WriteLog("INF", $"Unzip : {FilePath}");

                        using (ZipFile zipFile = ZipFile.Read(FilePath))
                        {
                            string PathZip = FilePath.Replace(".zip", "");

                            zipFile.ExtractAll(PathZip,
                            Ionic.Zip.ExtractExistingFileAction.DoNotOverwrite);

                            DirectoryInfo directoryInfo = new DirectoryInfo(PathZip);
                            FileInfo[] Files = directoryInfo.GetFiles("*.txt");

                            foreach (FileInfo file in Files)
                            {
                                DataRow dr = dtReturn.NewRow();
                                dr["ZIP_FILE_NAME"] = FileName;
                                dr["FILE_PATH"] = file.FullName;
                                dr["FILE_NAME"] = Path.GetFileName(file.FullName);

                                dtReturn.Rows.Add(dr);

                                LogHelper.WriteLog("INF", $"Unzip files : {file.FullName}");
                            }
                        }
                    }
                    else if (string.Equals(FileExt.ToLower(), ".rar"))
                    {
                        LogHelper.WriteLog("INF", $"Unrar : {FilePath}");

                        string PathRar = FilePath.Replace(".rar", "");
                        if (!System.IO.Directory.Exists(PathRar))
                        {
                            System.IO.Directory.CreateDirectory(PathRar);
                        }

                        var archive = RarArchive.Open(FilePath);

                        foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
                            entry.WriteToDirectory(PathRar);

                        DirectoryInfo directoryInfo = new DirectoryInfo(PathRar);
                        FileInfo[] Files = directoryInfo.GetFiles("*.txt");

                        foreach (FileInfo file in Files)
                        {
                            DataRow dr = dtReturn.NewRow();
                            dr["ZIP_FILE_NAME"] = FileName;
                            dr["FILE_PATH"] = file.FullName;
                            dr["FILE_NAME"] = Path.GetFileName(file.FullName);

                            dtReturn.Rows.Add(dr);

                            LogHelper.WriteLog("INF", $"Unrar files : {file.FullName}");
                        }
                    }
                    else if (string.Equals(FileExt.ToLower(), ".txt"))
                    {
                        DataRow dr = dtReturn.NewRow();
                        dr["FILE_PATH"] = FilePath;
                        dr["FILE_NAME"] = FileName;

                        dtReturn.Rows.Add(dr);

                        LogHelper.WriteLog("INF", $"files : {FilePath}");
                    }
                }

                for (int k = 0; k < dtReturn.Rows.Count; k++)
                {
                    try
                    {
                        string FilePath = dtReturn.Rows[k]["FILE_PATH"].ToString();

                        DataTable dtImport = new DataTable();

                        if (!System.IO.File.Exists(FilePath))
                        {
                            dtReturn.Rows[k]["RESULT"] = "E";
                            dtReturn.Rows[k]["MESSAGE"] = "File not found.";

                            LogHelper.WriteLog("ERR", $"File not found. : {FilePath}");

                            continue;
                        }

                        if (string.Equals(BankShortName.ToUpper(), "BAAC"))
                        {
                            string[] lines = System.IO.File.ReadAllLines(FilePath, System.Text.Encoding.GetEncoding(874));

                            for (int i = 0; i < lines.Length; i++)
                            {
                                string[] cols = lines[i].Split('|');

                                if (i == 0)
                                {// Create DataTable Column
                                    for (int j = 0; j < cols.Length; j++)
                                    {
                                        dtImport.Columns.Add($"COL_{j + 1}");
                                    }
                                }

                                dtImport.Rows.Add(
                                          cols[0].Trim()
                                        , cols[1].Trim()
                                        , cols[2].Trim()
                                        , cols[3].Trim()
                                        , cols[4].Trim()
                                        , cols[5].Trim()
                                        , cols[6].Trim()
                                        , cols[7].Trim()
                                        , cols[8].Trim()
                                        , cols[9].Trim()
                                        , cols[10].Trim()
                                        , cols[11].Trim()
                                        , cols[12].Trim()
                                        , cols[13].Trim()
                                        , cols[14].Trim()
                                        , cols[15].Trim()
                                        , cols[16].Trim()
                                        , cols[17].Trim()
                                        , cols[18].Trim()
                                        , cols[19].Trim()
                                        );
                            }

                            //Insert to DB

                        }
                        else if (string.Equals(BankShortName.ToUpper(), "KBANK"))
                        {

                        }
                        else if (string.Equals(BankShortName.ToUpper(), "KTB"))
                        {

                        }
                    }
                    catch (Exception ex)
                    {
                        dtReturn.Rows[k]["RESULT"] = "E";
                        dtReturn.Rows[k]["MESSAGE"] = "Exception : " + ex.Message;

                        LogHelper.WriteLog("ERR", $"Exception : {ex.Message}");
                    }

                    //Upload File to S3
                    //for (int i = 0; i < dtReturn.Rows.Count; i++)
                    //{
                    //    Stream stream = System.IO.File.OpenRead(dtReturn.Rows[i]["FILE_PATH"].ToString());

                    //    AmazonHelper.UploadFileToS3(
                    //          stream
                    //        , dtReturn.Rows[i]["FILE_NAME"].ToString()
                    //        , Path.GetDirectoryName(dtReturn.Rows[i]["FILE_NAME"].ToString())
                    //        );
                    //}
                }

                json = JsonConvert.SerializeObject(dtReturn, Formatting.None);
                response.Content = new StringContent(json, Encoding.UTF8, "application/json");

                return Ok(json);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("ERR", "Call.. api/BankStatement/ImportFile/Import exception : " + ex.Message);

                response.Content = new StringContent(ex.Message, Encoding.UTF8, "application/json");
                return BadRequest(ex.Message);
            }
            finally
            {
                LogHelper.WriteLog("INF", "Call Ended.. api/BankStatement/ImportFile/Import");
            }
        }

        private void UploadFileToS3()
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}