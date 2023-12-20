using CFRs.DAL.BANK_RECONCILE;
using System.Data;

namespace CFRs.BLL.BANK_RECONCILE
{
    public class SourceImport_BLL
    {
        public DataTable EBAO_LS_ImportBLL(DataTable dtImport, string DateStart, string DateEnd, string Mode)
        {
            try
            {
                return SourceImport_DAL.Instance.EBAO_LS_ImportDAL(dtImport, DateStart, DateEnd, Mode);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable EBAO_LS_GROUP_KRU_ImportBLL(DataTable dtImport, string DateStart, string DateEnd, string Mode)
        {
            try
            {
                return SourceImport_DAL.Instance.EBAO_LS_Group_Kru_ImportDAL(dtImport, DateStart, DateEnd, Mode);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable EBAO_GLS_ImportBLL(DataTable dtImport, string DateStart, string DateEnd, string Mode)
        {
            try
            {
                return SourceImport_DAL.Instance.EBAO_GLS_ImportDAL(dtImport, DateStart, DateEnd, Mode);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable DMS_ImportBLL(DataTable dtImport, string DateStart, string DateEnd, string Mode)
        {
            try
            {
                return SourceImport_DAL.Instance.DMS_ImportDAL(dtImport, DateStart, DateEnd, Mode);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable SMART_RP_ImportBLL(DataTable dtImport, string DateStart, string DateEnd, string Mode)
        {
            try
            {
                return SourceImport_DAL.Instance.SMART_RP_ImportDAL(dtImport, DateStart, DateEnd, Mode);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable SAP_ImportBLL(DataTable dtImport, string DateStart, string DateEnd, string Mode)
        {
            try
            {
                return SourceImport_DAL.Instance.SAP_ImportDAL(dtImport, DateStart, DateEnd, Mode);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable GetImportBLL(string Condition)
        {
            try
            {
                return SourceImport_DAL.Instance.GetImportDAL(Condition);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable GetImportSAP_BLL(string Condition)
        {
            try
            {
                return SourceImport_DAL.Instance.GetImportSAP_DAL(Condition);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region + Instance +
        private static SourceImport_BLL _instance;
        public static SourceImport_BLL Instance
        {
            get
            {
                _instance = new SourceImport_BLL();
                return _instance;
            }
        }
        #endregion
    }
}