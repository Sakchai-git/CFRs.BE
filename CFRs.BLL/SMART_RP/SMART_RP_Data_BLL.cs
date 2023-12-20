using CFRs.DAL.SMART_RP;
using System.Data;

namespace CFRs.BLL.SMART_RP
{
    public class SMART_RP_Data_BLL
    {
        public DataTable GetDataBLL(string DateStart, string DateEnd)
        {
            try
            {
                return SMART_RP_Data_DAL.Instance.GetDataDAL(DateStart, DateEnd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region + Instance +
        private static SMART_RP_Data_BLL _instance;
        public static SMART_RP_Data_BLL Instance
        {
            get
            {
                _instance = new SMART_RP_Data_BLL();
                return _instance;
            }
        }
        #endregion
    }
}