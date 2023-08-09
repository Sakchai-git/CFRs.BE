using BankReconcile.DAL.ConnectionDAL;
using System.Data;
using System.Data.SqlClient;

namespace BankReconcile.DAL.BANK_RECONCILE
{
    public class Token_DAL : CFRs_DAL
    {
        public DataTable CheckLoginDAL(string Username, string Password)
        {
            try
            {
                DataTable dtReturn = new DataTable();
                string Query = $"SELECT * " +
                   $"            FROM M_USER_API" +
                   $"            WHERE USERNAME = '{Username}'" +
                   $"            AND PASSWORD = '{Password}'" +
                   $"            AND IS_ACTIVE = 1";

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
        private static Token_DAL _instance;
        public static Token_DAL Instance
        {
            get
            {
                _instance = new Token_DAL();
                return _instance;
            }
        }
        #endregion
    }
}