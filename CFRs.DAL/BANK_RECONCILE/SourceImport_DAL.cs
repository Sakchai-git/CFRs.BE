using BankReconcile.DAL.ConnectionDAL;
using CFRs.DAL.Helper;
using System.Data;
using System.Data.SqlClient;

namespace CFRs.DAL.BANK_RECONCILE
{
    public class SourceImport_DAL : CFRs_DAL
    {
        public DataTable EBAO_LS_ImportDAL(DataTable dtImport, string DateStart, string DateEnd, string Mode)
        {
            try
            {
                SQLConn.Open();

                string Query = String.Empty;

                Query = "TRUNCATE TABLE T_EBAO_LS_TEMP";

                SqlCommand cmdInserTemp = new SqlCommand(Query, SQLConn);
                cmdInserTemp.CommandType = CommandType.Text;

                cmdInserTemp.ExecuteNonQuery();

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(GetSettingsHelper.CFRsConnectionString))
                {
                    bulkCopy.DestinationTableName = "T_EBAO_LS_TEMP";

                    bulkCopy.WriteToServer(dtImport);
                }

                DataTable dtReturn = new DataTable();
                Query = "USP_T_EBAO_LS_IMPORT";

                SqlTransaction tr = SQLConn.BeginTransaction();

                try
                {
                    SqlCommand cmdInsert = new SqlCommand(Query, SQLConn);
                    cmdInsert.CommandType = CommandType.StoredProcedure;
                    cmdInsert.CommandTimeout = 500;

                    cmdInsert.Parameters.AddWithValue("@I_DATE_START", DateStart);
                    cmdInsert.Parameters.AddWithValue("@I_DATE_END", DateEnd);
                    cmdInsert.Parameters.AddWithValue("@I_MODE", Mode);

                    cmdInsert.Transaction = tr;

                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmdInsert))
                    {
                        dataAdapter.Fill(dtReturn);
                    }

                    tr.Commit();
                }
                catch (Exception ex)
                {
                    tr.Rollback();
                    throw new Exception(ex.Message);
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

        public DataTable EBAO_LS_Group_Kru_ImportDAL(DataTable dtImport, string DateStart, string DateEnd, string Mode)
        {
            try
            {
                SQLConn.Open();

                string Query = String.Empty;

                Query = "TRUNCATE TABLE T_GROUP_KRU_TEMP";

                SqlCommand cmdInserTemp = new SqlCommand(Query, SQLConn);
                cmdInserTemp.CommandType = CommandType.Text;

                cmdInserTemp.ExecuteNonQuery();

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(GetSettingsHelper.CFRsConnectionString))
                {
                    bulkCopy.DestinationTableName = "T_GROUP_KRU_TEMP";

                    bulkCopy.WriteToServer(dtImport);
                }

                DataTable dtReturn = new DataTable();
                Query = "USP_T_GROUP_KRU_IMPORT";

                SqlTransaction tr = SQLConn.BeginTransaction();

                try
                {
                    SqlCommand cmdInsert = new SqlCommand(Query, SQLConn);
                    cmdInsert.CommandType = CommandType.StoredProcedure;
                    cmdInsert.CommandTimeout = 500;

                    cmdInsert.Parameters.AddWithValue("@I_DATE_START", DateStart);
                    cmdInsert.Parameters.AddWithValue("@I_DATE_END", DateEnd);
                    cmdInsert.Parameters.AddWithValue("@I_MODE", Mode);

                    cmdInsert.Transaction = tr;

                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmdInsert))
                    {
                        dataAdapter.Fill(dtReturn);
                    }

                    tr.Commit();
                }
                catch (Exception ex)
                {
                    tr.Rollback();
                    throw new Exception(ex.Message);
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

        public DataTable EBAO_GLS_ImportDAL(DataTable dtImport, string DateStart, string DateEnd, string Mode)
        {
            try
            {
                SQLConn.Open();

                string Query = String.Empty;

                Query = "TRUNCATE TABLE T_EBAO_GLS_TEMP";

                SqlCommand cmdInserTemp = new SqlCommand(Query, SQLConn);
                cmdInserTemp.CommandType = CommandType.Text;

                cmdInserTemp.ExecuteNonQuery();

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(GetSettingsHelper.CFRsConnectionString))
                {
                    bulkCopy.DestinationTableName = "T_EBAO_GLS_TEMP";

                    bulkCopy.WriteToServer(dtImport);
                }

                DataTable dtReturn = new DataTable();
                Query = "USP_T_EBAO_GLS_IMPORT";

                SqlTransaction tr = SQLConn.BeginTransaction();

                try
                {
                    SqlCommand cmdInsert = new SqlCommand(Query, SQLConn);
                    cmdInsert.CommandType = CommandType.StoredProcedure;
                    cmdInsert.CommandTimeout = 500;

                    cmdInsert.Parameters.AddWithValue("@I_DATE_START", DateStart);
                    cmdInsert.Parameters.AddWithValue("@I_DATE_END", DateEnd);
                    cmdInsert.Parameters.AddWithValue("@I_MODE", Mode);

                    cmdInsert.Transaction = tr;

                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmdInsert))
                    {
                        dataAdapter.Fill(dtReturn);
                    }

                    tr.Commit();
                }
                catch (Exception ex)
                {
                    tr.Rollback();
                    throw new Exception(ex.Message);
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

        public DataTable DMS_ImportDAL(DataTable dtImport, string DateStart, string DateEnd, string Mode)
        {
            try
            {
                SQLConn.Open();

                string Query = String.Empty;

                Query = "TRUNCATE TABLE T_DMS_TEMP";

                SqlCommand cmdInserTemp = new SqlCommand(Query, SQLConn);
                cmdInserTemp.CommandType = CommandType.Text;

                cmdInserTemp.ExecuteNonQuery();

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(GetSettingsHelper.CFRsConnectionString))
                {
                    bulkCopy.DestinationTableName = "T_DMS_TEMP";

                    bulkCopy.WriteToServer(dtImport);
                }

                DataTable dtReturn = new DataTable();
                Query = "USP_T_DMS_IMPORT";

                SqlTransaction tr = SQLConn.BeginTransaction();

                try
                {
                    SqlCommand cmdInsert = new SqlCommand(Query, SQLConn);
                    cmdInsert.CommandType = CommandType.StoredProcedure;
                    cmdInsert.CommandTimeout = 500;

                    cmdInsert.Parameters.AddWithValue("@I_DATE_START", DateStart);
                    cmdInsert.Parameters.AddWithValue("@I_DATE_END", DateEnd);
                    cmdInsert.Parameters.AddWithValue("@I_MODE", Mode);

                    cmdInsert.Transaction = tr;

                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmdInsert))
                    {
                        dataAdapter.Fill(dtReturn);
                    }

                    tr.Commit();
                }
                catch (Exception ex)
                {
                    tr.Rollback();
                    throw new Exception(ex.Message);
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

        public DataTable SMART_RP_ImportDAL(DataTable dtImport, string DateStart, string DateEnd, string Mode)
        {
            try
            {
                SQLConn.Open();

                string Query = String.Empty;

                Query = "TRUNCATE TABLE T_SMART_RP_TEMP";

                SqlCommand cmdInserTemp = new SqlCommand(Query, SQLConn);
                cmdInserTemp.CommandType = CommandType.Text;

                cmdInserTemp.ExecuteNonQuery();

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(GetSettingsHelper.CFRsConnectionString))
                {
                    bulkCopy.DestinationTableName = "T_SMART_RP_TEMP";

                    bulkCopy.WriteToServer(dtImport);
                }

                DataTable dtReturn = new DataTable();
                Query = "USP_T_SMART_RP_IMPORT";

                SqlTransaction tr = SQLConn.BeginTransaction();

                try
                {
                    SqlCommand cmdInsert = new SqlCommand(Query, SQLConn);
                    cmdInsert.CommandType = CommandType.StoredProcedure;
                    cmdInsert.CommandTimeout = 500;

                    cmdInsert.Parameters.AddWithValue("@I_DATE_START", DateStart);
                    cmdInsert.Parameters.AddWithValue("@I_DATE_END", DateEnd);
                    cmdInsert.Parameters.AddWithValue("@I_MODE", Mode);

                    cmdInsert.Transaction = tr;

                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmdInsert))
                    {
                        dataAdapter.Fill(dtReturn);
                    }

                    tr.Commit();
                }
                catch (Exception ex)
                {
                    tr.Rollback();
                    throw new Exception(ex.Message);
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

        public DataTable SAP_ImportDAL(DataTable dtImport, string DateStart, string DateEnd, string Mode)
        {
            try
            {
                SQLConn.Open();

                string Query = String.Empty;

                Query = "TRUNCATE TABLE T_SAP_TEMP";

                SqlCommand cmdInserTemp = new SqlCommand(Query, SQLConn);
                cmdInserTemp.CommandType = CommandType.Text;

                cmdInserTemp.ExecuteNonQuery();

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(GetSettingsHelper.CFRsConnectionString))
                {
                    bulkCopy.DestinationTableName = "T_SAP_TEMP";

                    bulkCopy.WriteToServer(dtImport);
                }

                DataTable dtReturn = new DataTable();
                Query = "USP_T_SAP_IMPORT";

                SqlTransaction tr = SQLConn.BeginTransaction();

                try
                {
                    SqlCommand cmdInsert = new SqlCommand(Query, SQLConn);
                    cmdInsert.CommandType = CommandType.StoredProcedure;
                    cmdInsert.CommandTimeout = 500;

                    cmdInsert.Parameters.AddWithValue("@I_DATE_START", DateStart);
                    cmdInsert.Parameters.AddWithValue("@I_DATE_END", DateEnd);
                    cmdInsert.Parameters.AddWithValue("@I_MODE", Mode);

                    cmdInsert.Transaction = tr;

                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmdInsert))
                    {
                        dataAdapter.Fill(dtReturn);
                    }

                    tr.Commit();
                }
                catch (Exception ex)
                {
                    tr.Rollback();
                    throw new Exception(ex.Message);
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

        public DataTable GetImportDAL(string Condition)
        {
            try
            {
                DataTable dtReturn = new DataTable();
                string Query = $"SELECT * " +
                   $"            FROM VW_T_SOURCE_IMPORT" +
                   $"            WHERE 1=1 " + Condition;

                SQLConn.Open();
                SqlCommand cmd = new SqlCommand(Query, SQLConn);
                cmd.CommandTimeout = 500;

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

        public DataTable GetImportSAP_DAL(string Condition)
        {
            try
            {
                DataTable dtReturn = new DataTable();
                string Query = $"SELECT * " +
                   $"            FROM VW_T_SOURCE_IMPORT_SAP" +
                   $"            WHERE 1=1 " + Condition;

                SQLConn.Open();
                SqlCommand cmd = new SqlCommand(Query, SQLConn);
                cmd.CommandTimeout = 500;

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
        private static SourceImport_DAL _instance;
        public static SourceImport_DAL Instance
        {
            get
            {
                _instance = new SourceImport_DAL();
                return _instance;
            }
        }
        #endregion
    }
}