using CFRs.DAL;
using Oracle.ManagedDataAccess.Client;

namespace BankReconcile.DAL.ConnectionDAL
{
    public partial class EBAO_LS_DAL
    {
        protected OracleConnection OraConn = new OracleConnection(DALSetting.Default.EBAO_LS_ConnectionString);
    }
}