using System.Data.SqlClient;

namespace CFRs.DAL.ConnectionDAL
{
    public class SMART_RP_DAL
    {
        protected SqlConnection SQLConn = new SqlConnection(DALSetting.Default.SMART_RP_ConnectionString);
    }
}