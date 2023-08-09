using BankReconcile.DAL.ConnectionDAL;
using System.Data;
using System.Data.SqlClient;

namespace CFRs.DAL.CONFIG
{
    public class ConfigDAL : CFRs_DAL
    {
        public DataTable GetConfigDAL(string Condition)
        {
            try
            {
                DataTable dtReturn = new DataTable();
                string Query = $"SELECT * " +
                   $"            FROM VW_M_CONFIG" +
                   $"            WHERE 1=1 " + Condition;

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
        private static ConfigDAL _instance;
        public static ConfigDAL Instance
        {
            get
            {
                _instance = new ConfigDAL();
                return _instance;
            }
        }
        #endregion
    }
}