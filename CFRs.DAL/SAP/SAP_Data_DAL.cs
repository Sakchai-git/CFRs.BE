using CFRs.DAL.CONFIG;
using CFRs.DAL.ConnectionDAL;
using System.Data;
using System.Data.SqlClient;

namespace CFRs.DAL.SAP
{
    public partial class SAP_Data_DAL : SAP_DAL
    {
        public DataTable GetDataDAL(string DateStart, string DateEnd)
        {
            try
            {
                DataTable dtConfig = ConfigDAL.Instance.GetConfigDAL(" AND CONFIG_NAME = 'QUERY_SAP'");
                if (dtConfig.Rows.Count > 0)
                {
                    DataTable dtReturn = new DataTable();
                    string Query = dtConfig.Rows[0]["CONFIG_VALUE_1"].ToString();

                    Query = Query.Replace("{DATE_START}", DateStart);
                    Query = Query.Replace("{DATE_END}", DateEnd);

                    SqlCommand cmd = new SqlCommand(Query, SQLConn);
                    cmd.CommandTimeout = 3600;

                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd))
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
                if (SQLConn != null && SQLConn.State == ConnectionState.Open)
                {
                    SQLConn.Close();
                }
            }
        }

        #region + Instance +
        private static SAP_Data_DAL _instance;
        public static SAP_Data_DAL Instance
        {
            get
            {
                _instance = new SAP_Data_DAL();
                return _instance;
            }
        }
        #endregion
    }
}