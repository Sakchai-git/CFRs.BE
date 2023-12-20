using CFRs.BE.Helper;
using CFRs.BLL.BANK_RECONCILE;
using CFRs.BLL.EBAO;
using System.Data;
using System.Reflection;

namespace CFRs.BE.Batch
{
    public class Batch_EBAO_GLS
    {
        static object lockupdate = new object();

        public void Get()
        {
            lock (lockupdate)
            {
                try
                {
                    LogHelper.WriteLog("EBAO_GLS", "INF", $"Batch_EBAO_GLS Start");

                    string DateStart = BatchHelper.GetBatchDateStart("EBAO_GLS_BatchFixDate");
                    string DateEnd = BatchHelper.GetBatchDateEnd("EBAO_GLS_BatchFixDate");
                    string Mode = BatchHelper.GetBatchMode("EBAO_GLS_BatchFixDate");

                    //Get data from EBAO LS
                    LogHelper.WriteLog("EBAO_GLS", "INF", $"Get data from EBAO GLS (Date : {DateStart} to {DateEnd})");
                    DataTable dtImport = EBAO_GLS_Data_BLL.Instance.GetDataBLL(DateStart, DateEnd);

                    //Write to DB
                    LogHelper.WriteLog("EBAO_GLS", "INF", $"Found Data {dtImport.Rows.Count} Records.");

                    if (dtImport.Rows.Count > 0)
                    {
                        LogHelper.WriteLog("EBAO_GLS", "INF", $"Write to DB");

                        SourceImport_BLL.Instance.EBAO_GLS_ImportBLL(dtImport, DateStart, DateEnd, Mode);

                        LogHelper.WriteLog("EBAO_GLS", "INF", $"Write to DB Success.");
                    }

                    LogHelper.WriteLog("EBAO_GLS", "INF", $"Batch_EBAO_GLS End");
                }
                catch (Exception ex)
                {
                    string MethodName = MethodBase.GetCurrentMethod().Name;
                    LogHelper.WriteLog("EBAO_GLS", "ERR", $"{MethodName} : {ex.Message}");
                }
            }
        }
    }
}