using BankReconcile.DAL.OSC;
using System.Data;

namespace BankReconcile.BLL.OSC
{
    public class OSC_Cheque_BLL
    {
        public DataTable GetChequeBLL()
        {
            try
            {
                return OSC_Cheque_DAL.Instance.GetChequeDAL();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region + Instance +
        private static OSC_Cheque_BLL _instance;
        public static OSC_Cheque_BLL Instance
        {
            get
            {
                _instance = new OSC_Cheque_BLL();
                return _instance;
            }
        }
        #endregion
    }
}