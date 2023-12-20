using CFRs.BE.Helper;
using CFRs.BLL.BANK_RECONCILE;
using CFRs.BLL.SAP;
using System.Data;
using System.Reflection;

namespace CFRs.BE.Batch
{
    public class Batch_SAP
    {
        static object lockupdate = new object();

        public void Get()
        {
            lock (lockupdate)
            {
                try
                {
                    LogHelper.WriteLog("SAP", "INF", $"Batch_SAP Start");

                    string DateStart = BatchHelper.GetBatchDateStart("SAP_BatchFixDate");
                    string DateEnd = BatchHelper.GetBatchDateEnd("SAP_BatchFixDate");
                    string Mode = BatchHelper.GetBatchMode("SAP_BatchFixDate");

                    //Get data from SAP
                    LogHelper.WriteLog("SAP", "INF", $"Get data from SAP (Date : {DateStart} to {DateEnd})");
                    DataTable dtImport = SAP_Data_BLL.Instance.GetDataBLL(DateStart, DateEnd);

                    //Write to DB
                    LogHelper.WriteLog("SAP", "INF", $"Found Data {dtImport.Rows.Count} Records.");

                    if (dtImport.Rows.Count > 0)
                    {
                        LogHelper.WriteLog("SAP", "INF", $"Write to DB");

                        SourceImport_BLL.Instance.SAP_ImportBLL(dtImport, DateStart, DateEnd, Mode);

                        LogHelper.WriteLog("SAP", "INF", $"Write to DB Success.");
                    }

                    LogHelper.WriteLog("SAP", "INF", $"Batch_SAP End");
                }
                catch (Exception ex)
                {
                    string MethodName = MethodBase.GetCurrentMethod().Name;
                    LogHelper.WriteLog("SAP", "ERR", $"{MethodName} : {ex.Message}");
                }
            }
        }
    }
}