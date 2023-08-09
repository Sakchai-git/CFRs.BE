using BankReconcile.DAL.EBAO;
using System.Data;

namespace BankReconcile.BLL.EBAO
{
    public class EBAO_Data_BLL
    {
        public DataTable GetTestBLL()
        {
            try
            {
                return EBAO_Data_DAL.Instance.GetTestDAL();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region + Instance +
        private static EBAO_Data_BLL _instance;
        public static EBAO_Data_BLL Instance
        {
            get
            {
                _instance = new EBAO_Data_BLL();
                return _instance;
            }
        }
        #endregion
    }
}