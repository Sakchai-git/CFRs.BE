using CFRs.DAL.BANK_RECONCILE;
using System.Data;

namespace CFRs.BLL.BANK_RECONCILE
{
    public class StatementImport_BLL
    {
        public DataTable ImportBLL(int MonthID, int Year, string BankShortName
            , string PathLocal, string PathS3, int UserID, DataTable dtImport
            , string RowHeader, string RowFooter)
        {
            try
            {
                return StatementImport_DAL.Instance.ImportDAL(MonthID, Year, BankShortName, PathLocal, PathS3, UserID, dtImport
                    , RowHeader, RowFooter);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region + Instance +
        private static StatementImport_BLL _instance;
        public static StatementImport_BLL Instance
        {
            get
            {
                _instance = new StatementImport_BLL();
                return _instance;
            }
        }
        #endregion
    }
}