using BankReconcile.BLL.EBAO;
using CFRs.BE.Helper;
using CFRs.BLL.BANK_RECONCILE;
using System.Data;
using System.Reflection;

namespace CFRs.BE.Batch
{
    public class Batch_Group_Kru
    {
        static object lockupdate = new object();

        public void Get()
        {
            lock (lockupdate)
            {
                //TO_CHAR(osc.cheque_no_date, 'dd/MM/yyyy HH:mi:ss')
                try
                {
                    LogHelper.WriteLog("EBAO_LS (Group Kru)", "INF", $"Batch_Group_Kru Start");

                    string DateStart = BatchHelper.GetBatchDateStart("GROUP_KRU_BatchFixDate");
                    string DateEnd = BatchHelper.GetBatchDateEnd("GROUP_KRU_BatchFixDate");
                    string Mode = BatchHelper.GetBatchMode("GROUP_KRU_BatchFixDate");

                    //Get data from EBAO LS
                    LogHelper.WriteLog("EBAO LS (Group Kru)", "INF", $"Get data from EBAO LS (Group Kru) (Date : {DateStart} to {DateEnd})");
                    DataTable dtImport = EBAO_LS_Data_BLL.Instance.GetDataGroupKruBLL(DateStart, DateEnd);

                    //Write to DB
                    LogHelper.WriteLog("EBAO LS (Group Kru)", "INF", $"Found Data {dtImport.Rows.Count} Records.");

                    if (dtImport.Rows.Count > 0)
                    {
                        LogHelper.WriteLog("EBAO LS (Group Kru)", "INF", $"Write to DB");

                        SourceImport_BLL.Instance.EBAO_LS_GROUP_KRU_ImportBLL(dtImport, DateStart, DateEnd, Mode);

                        LogHelper.WriteLog("EBAO LS (Group Kru)", "INF", $"Write to DB Success.");
                    }

                    LogHelper.WriteLog("EBAO LS (Group Kru)", "INF", $"Batch_Group_Kru End");
                }
                catch (Exception ex)
                {
                    string MethodName = MethodBase.GetCurrentMethod().Name;
                    LogHelper.WriteLog("EBAO LS (Group Kru)", "ERR", $"{MethodName} : {ex.Message}");
                }
            }
        }
    }
}
