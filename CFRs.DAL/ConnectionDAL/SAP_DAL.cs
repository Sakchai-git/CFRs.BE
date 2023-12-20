using System.Data.SqlClient;

namespace CFRs.DAL.ConnectionDAL
{
    public partial class SAP_DAL
    {
        protected SqlConnection SQLConn = new SqlConnection(DALSetting.Default.SAP_ConnectionString);
    }
}