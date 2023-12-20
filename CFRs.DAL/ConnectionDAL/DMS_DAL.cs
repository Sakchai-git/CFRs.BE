using Oracle.ManagedDataAccess.Client;

namespace CFRs.DAL.ConnectionDAL
{
    public class DMS_DAL
    {
        protected OracleConnection OraConn = new OracleConnection(DALSetting.Default.DMS_ConnectionString);
    }
}