using Oracle.ManagedDataAccess.Client;

namespace CFRs.DAL.ConnectionDAL
{
    public class EBAO_GLS_DAL
    {
        protected OracleConnection OraConn = new OracleConnection(DALSetting.Default.EBAO_GLS_ConnectionString);
    }
}