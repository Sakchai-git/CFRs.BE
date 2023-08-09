using BankReconcile.DAL.ConnectionDAL;
using System.Data;
using System.Data.SqlClient;

namespace BankReconcile.DAL.OSC
{
    public partial class OSC_Cheque_DAL : OSC_DAL
    {
        public DataTable GetChequeDAL()
        {
            try
            {
                DataTable dtReturn = new DataTable();
                string Query = $"SELECT TOP(10) * " +
                   $"            FROM T_CHEQUE";

                SQLConn.Open();
                SqlCommand cmd = new SqlCommand(Query, SQLConn);

                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd))
                {
                    dataAdapter.Fill(dtReturn);
                }

                return dtReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (SQLConn != null && SQLConn.State == ConnectionState.Open)
                {
                    SQLConn.Close();
                }
            }
        }

        #region + Instance +
        private static OSC_Cheque_DAL _instance;
        public static OSC_Cheque_DAL Instance
        {
            get
            {
                _instance = new OSC_Cheque_DAL();
                return _instance;
            }
        }
        #endregion
    }
}