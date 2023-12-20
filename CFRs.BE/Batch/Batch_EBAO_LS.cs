using BankReconcile.BLL.EBAO;
using CFRs.BE.Helper;
using CFRs.BLL.BANK_RECONCILE;
using System.Data;
using System.Reflection;

namespace CFRs.BE.Batch
{
    public class Batch_EBAO_LS
    {
        static object lockupdate = new object();

        public void Get()
        {
            lock (lockupdate)
            {
                //TO_CHAR(osc.cheque_no_date, 'dd/MM/yyyy HH:mi:ss')
                try
                {
                    LogHelper.WriteLog("EBAO_LS", "INF", $"Batch_EBAO_LS Start");

                    string DateStart = BatchHelper.GetBatchDateStart("EBAO_LS_BatchFixDate");
                    string DateEnd = BatchHelper.GetBatchDateEnd("EBAO_LS_BatchFixDate");
                    string Mode = BatchHelper.GetBatchMode("EBAO_LS_BatchFixDate");

                    //Get data from EBAO LS
                    LogHelper.WriteLog("EBAO_LS", "INF", $"Get data from EBAO LS (Date : {DateStart} to {DateEnd})");
                    DataTable dtImport = EBAO_LS_Data_BLL.Instance.GetDataBLL(DateStart, DateEnd);

                    //Write to DB
                    LogHelper.WriteLog("EBAO_LS", "INF", $"Found Data {dtImport.Rows.Count} Records.");

                    if (dtImport.Rows.Count > 0)
                    {
                        LogHelper.WriteLog("EBAO_LS", "INF", $"Write to DB");

                        SourceImport_BLL.Instance.EBAO_LS_ImportBLL(dtImport, DateStart, DateEnd, Mode);

                        LogHelper.WriteLog("EBAO_LS", "INF", $"Write to DB Success.");
                    }

                    LogHelper.WriteLog("EBAO_LS", "INF", $"Batch_EBAO_LS End");
                }
                catch (Exception ex)
                {
                    string MethodName = MethodBase.GetCurrentMethod().Name;
                    LogHelper.WriteLog("EBAO_LS", "ERR", $"{MethodName} : {ex.Message}");
                }
            }
        }
    }
}