using CFRs.BE.Helper;
using CFRs.BLL.BANK_RECONCILE;
using CFRs.BLL.DMS;
using System.Data;
using System.Reflection;

namespace CFRs.BE.Batch
{
    public class Batch_DMS
    {
        static object lockupdate = new object();

        public void Get()
        {
            lock (lockupdate)
            {
                try
                {
                    LogHelper.WriteLog("DMS", "INF", $"Batch_DMS Start");

                    string DateStart = BatchHelper.GetBatchDateStart("DMS_BatchFixDate");
                    string DateEnd = BatchHelper.GetBatchDateEnd("DMS_BatchFixDate");
                    string Mode = BatchHelper.GetBatchMode("DMS_BatchFixDate");

                    //Get data from DMS
                    LogHelper.WriteLog("DMS", "INF", $"Get data from DMS (Date : {DateStart} to {DateEnd})");
                    DataTable dtImport = DMS_Data_BLL.Instance.GetDataBLL(DateStart, DateEnd);

                    //Write to DB
                    LogHelper.WriteLog("DMS", "INF", $"Found Data {dtImport.Rows.Count} Records.");

                    if (dtImport.Rows.Count > 0)
                    {
                        LogHelper.WriteLog("DMS", "INF", $"Write to DB");

                        SourceImport_BLL.Instance.DMS_ImportBLL(dtImport, DateStart, DateEnd, Mode);

                        LogHelper.WriteLog("DMS", "INF", $"Write to DB Success.");
                    }

                    LogHelper.WriteLog("DMS", "INF", $"Batch_DMS End");
                }
                catch (Exception ex)
                {
                    string MethodName = MethodBase.GetCurrentMethod().Name;
                    LogHelper.WriteLog("DMS", "ERR", $"{MethodName} : {ex.Message}");
                }
            }
        }
    }
}