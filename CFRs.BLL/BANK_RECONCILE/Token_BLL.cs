using BankReconcile.DAL.BANK_RECONCILE;
using System.Data;

namespace BankReconcile.BLL.BANK_RECONCILE
{
    public class Token_BLL
    {
        public DataTable CheckLoginBLL(string Username, string Password)
        {
            try
            {
                return Token_DAL.Instance.CheckLoginDAL(Username, Password);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region + Instance +
        private static Token_BLL _instance;
        public static Token_BLL Instance
        {
            get
            {
                _instance = new Token_BLL();
                return _instance;
            }
        }
        #endregion
    }
}