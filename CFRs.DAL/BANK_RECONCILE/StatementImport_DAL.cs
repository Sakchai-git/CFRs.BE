using BankReconcile.DAL.ConnectionDAL;
using System.Data;
using System.Data.SqlClient;

namespace CFRs.DAL.BANK_RECONCILE
{
    public partial class StatementImport_DAL : CFRs_DAL
    {
        public DataTable ImportDAL(int MonthID, int Year, string BankShortName
            , string PathLocal, string PathS3, int UserID, DataTable dtImport
            , string RowHeader, string RowFooter)
        {
            SQLConn.Open();
            SqlTransaction tr = SQLConn.BeginTransaction();

            try
            {
                DataTable dtReturn = new DataTable();
                string Query = "USP_T_STATEMENT_IMPORT";

                SqlCommand cmd = new SqlCommand(Query, SQLConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 300;

                DataSet ds = new DataSet();
                dtImport.TableName = "dt";
                ds.Tables.Add(dtImport.Copy());
                ds.DataSetName = "ds";

                cmd.Transaction = tr;

                cmd.Parameters.AddWithValue("@I_MONTH_ID", MonthID);
                cmd.Parameters.AddWithValue("@I_YEAR", Year);
                cmd.Parameters.AddWithValue("@I_BANK_SHORT_NAME", BankShortName);
                cmd.Parameters.AddWithValue("@I_PATH_LOCAL", PathLocal);
                cmd.Parameters.AddWithValue("@I_PATH_S3", PathS3);
                cmd.Parameters.AddWithValue("@I_USER_ID", UserID);
                cmd.Parameters.AddWithValue("@I_XML", ds.GetXml());
                cmd.Parameters.AddWithValue("@I_ROW_HEADER", RowHeader);
                cmd.Parameters.AddWithValue("@I_ROW_FOOTER", RowFooter);

                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd))
                {
                    dataAdapter.Fill(dtReturn);
                }

                tr.Commit();

                return dtReturn;
            }
            catch (Exception ex)
            {
                tr.Rollback();
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
        private static StatementImport_DAL _instance;
        public static StatementImport_DAL Instance
        {
            get
            {
                _instance = new StatementImport_DAL();
                return _instance;
            }
        }
        #endregion
    }
}
