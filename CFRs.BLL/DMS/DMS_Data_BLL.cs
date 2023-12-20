using CFRs.DAL.DMS;
using System.Data;

namespace CFRs.BLL.DMS
{
    public class DMS_Data_BLL
    {
        public DataTable GetDataBLL(string DateStart, string DateEnd)
        {
            try
            {
                return DMS_Data_DAL.Instance.GetDataDAL(DateStart, DateEnd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region + Instance +
        private static DMS_Data_BLL _instance;
        public static DMS_Data_BLL Instance
        {
            get
            {
                _instance = new DMS_Data_BLL();
                return _instance;
            }
        }
        #endregion
    }
}