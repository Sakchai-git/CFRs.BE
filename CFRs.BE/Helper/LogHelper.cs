using Serilog;

namespace CFRs.BE.Helper
{
    public static class LogHelper
    {
        public static void WriteLog(string LogType, string Message)
        {
            string LogFile = "Logs//CFRs_Log_.txt";

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(LogFile, rollingInterval: RollingInterval.Hour, rollOnFileSizeLimit: true)
                .CreateLogger();

            try
            {
                if (string.Equals(LogType, "INF"))
                    Log.Information(Message, "[INF]");
                else if (string.Equals(LogType, "ERR"))
                    Log.Error(Message, "[ERR]");
            }
            catch (Exception exception)
            {
                if (string.Equals(LogType, "INF"))
                    Log.Information(Message, "[INF]");
                else if (string.Equals(LogType, "ERR"))
                    Log.Error(Message, "[ERR]");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}