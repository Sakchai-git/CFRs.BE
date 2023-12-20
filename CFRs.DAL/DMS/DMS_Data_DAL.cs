using CFRs.DAL.CONFIG;
using CFRs.DAL.ConnectionDAL;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace CFRs.DAL.DMS
{
    public class DMS_Data_DAL : DMS_DAL
    {
        public DataTable GetDataDAL(string DateStart, string DateEnd)
        {
            try
            {
                DataTable dtConfig = ConfigDAL.Instance.GetConfigDAL(" AND CONFIG_NAME = 'QUERY_DMS'");
                if (dtConfig.Rows.Count > 0)
                {
                    DataTable dtReturn = new DataTable();
                    string Query = dtConfig.Rows[0]["CONFIG_VALUE_1"].ToString();

                    Query = Query.Replace("{DATE_START}", DateStart);
                    Query = Query.Replace("{DATE_END}", DateEnd);

                    OracleCommand cmd = new OracleCommand(Query, OraConn);
                    cmd.CommandTimeout = 3600;

                    using (OracleDataAdapter dataAdapter = new OracleDataAdapter(cmd))
                    {
                        dataAdapter.Fill(dtReturn);
                    }

                    return dtReturn;
                }
                else
                {
                    throw new Exception("Not found query in table M_CONFIG.");
                }
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
        private static DMS_Data_DAL _instance;
        public static DMS_Data_DAL Instance
        {
            get
            {
                _instance = new DMS_Data_DAL();
                return _instance;
            }
        }
        #endregion
    }
}