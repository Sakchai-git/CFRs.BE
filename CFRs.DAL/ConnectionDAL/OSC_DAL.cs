using CFRs.DAL;
using System.Data.SqlClient;

namespace BankReconcile.DAL.ConnectionDAL
{
    public partial class OSC_DAL
    {
        protected SqlConnection SQLConn = new SqlConnection(DALSetting.Default.OSCConnectionString);
    }
}