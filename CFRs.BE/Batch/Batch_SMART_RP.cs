using CFRs.BE.Helper;
using CFRs.BLL.BANK_RECONCILE;
using CFRs.BLL.SMART_RP;
using System.Data;
using System.Reflection;

namespace CFRs.BE.Batch
{
    public class Batch_SMART_RP
    {
        static object lockupdate = new object();

        public void Get()
        {
            lock (lockupdate)
            {
                try
                {
                    LogHelper.WriteLog("SMART_RP", "INF", $"Batch_SMART_RP Start");

                    string DateStart = BatchHelper.GetBatchDateStart("SMART_RP_BatchFixDate");
                    string DateEnd = BatchHelper.GetBatchDateEnd("SMART_RP_BatchFixDate");
                    string Mode = BatchHelper.GetBatchMode("SMART_RP_BatchFixDate");

                    //Get data from DMS
                    LogHelper.WriteLog("SMART_RP", "INF", $"Get data from Smart RP (Date : {DateStart} to {DateEnd})");
                    DataTable dtImport = SMART_RP_Data_BLL.Instance.GetDataBLL(DateStart, DateEnd);

                    //Write to DB
                    LogHelper.WriteLog("SMART_RP", "INF", $"Found Data {dtImport.Rows.Count} Records.");

                    if (dtImport.Rows.Count > 0)
                    {
                        LogHelper.WriteLog("SMART_RP", "INF", $"Write to DB");

                        SourceImport_BLL.Instance.SMART_RP_ImportBLL(dtImport, DateStart, DateEnd, Mode);

                        LogHelper.WriteLog("SMART_RP", "INF", $"Write to DB Success.");
                    }

                    LogHelper.WriteLog("SMART_RP", "INF", $"Batch_SMART_RP End");
                }
                catch (Exception ex)
                {
                    string MethodName = MethodBase.GetCurrentMethod().Name;
                    LogHelper.WriteLog("SMART_RP", "ERR", $"{MethodName} : {ex.Message}");
                }
            }
        }
    }
}