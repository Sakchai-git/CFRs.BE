using CFRs.DAL.EBAO;
using System.Data;

namespace CFRs.BLL.EBAO
{
    public class EBAO_GLS_Data_BLL
    {
        public DataTable GetDataBLL(string DateStart, string DateEnd)
        {
            try
            {
                return EBAO_GLS_Data_DAL.Instance.GetDataDAL(DateStart, DateEnd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region + Instance +
        private static EBAO_GLS_Data_BLL _instance;
        public static EBAO_GLS_Data_BLL Instance
        {
            get
            {
                _instance = new EBAO_GLS_Data_BLL();
                return _instance;
            }
        }
        #endregion
    }
}