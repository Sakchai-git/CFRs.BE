using CFRs.DAL.SAP;
using System.Data;

namespace CFRs.BLL.SAP
{
    public class SAP_Data_BLL
    {
        public DataTable GetDataBLL(string DateStart, string DateEnd)
        {
            try
            {
                return SAP_Data_DAL.Instance.GetDataDAL(DateStart, DateEnd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region + Instance +
        private static SAP_Data_BLL _instance;
        public static SAP_Data_BLL Instance
        {
            get
            {
                _instance = new SAP_Data_BLL();
                return _instance;
            }
        }
        #endregion
    }
}