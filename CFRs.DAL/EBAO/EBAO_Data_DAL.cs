using BankReconcile.DAL.ConnectionDAL;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace BankReconcile.DAL.EBAO
{
    public partial class EBAO_Data_DAL : EBAO_DAL
    {
        public DataTable GetTestDAL()
        {
            try
            {
                DataTable dtReturn = new DataTable();
                string Query = $"SELECT * FROM T_AGENT WHERE ROWNUM <= 10";

                OraConn.Open();
                OracleCommand cmd = new OracleCommand(Query, OraConn);

                using (OracleDataAdapter dataAdapter = new OracleDataAdapter(cmd))
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
                if (OraConn != null && OraConn.State == ConnectionState.Open)
                {
                    OraConn.Close();
                }
            }
        }

        #region + Instance +
        private static EBAO_Data_DAL _instance;
        public static EBAO_Data_DAL Instance
        {
            get
            {
                _instance = new EBAO_Data_DAL();
                return _instance;
            }
        }
        #endregion
    }
}