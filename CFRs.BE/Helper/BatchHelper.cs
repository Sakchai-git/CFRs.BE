using System.Globalization;

namespace CFRs.BE.Helper
{
    public static class BatchHelper
    {
        public static string GetBatchDateStart(string Type)
        {
            try
            {
                var builder = WebApplication.CreateBuilder();

                string DateStart = builder.Configuration[$"{Type}:DateStart"];
                if (string.IsNullOrEmpty(DateStart))
                {
                    DateStart = DateTime.Now.ToString("yyyyMMdd", new CultureInfo("en-US"));
                }

                return DateStart;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string GetBatchDateEnd(string Type)
        {
            try
            {
                var builder = WebApplication.CreateBuilder();

                string DateEnd = builder.Configuration[$"{Type}:DateEnd"];
                if (string.IsNullOrEmpty(DateEnd))
                {
                    DateEnd = DateTime.Now.ToString("yyyyMMdd", new CultureInfo("en-US"));
                }

                return DateEnd;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string GetBatchMode(string Type)
        {
            try
            {
                var builder = WebApplication.CreateBuilder();

                string Mode = builder.Configuration[$"{Type}:Mode"];
                if (string.IsNullOrEmpty(Mode))
                {
                    Mode = "1";
                }

                return Mode;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}