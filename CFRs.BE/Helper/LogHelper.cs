using Serilog;

namespace CFRs.BE.Helper
{
    public static class LogHelper
    {
        public static void WriteLog(string System, string LogType, string Message)
        {
            string LogFile = $"Logs_{System}//{System}_Log_.txt";

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(LogFile, rollingInterval: RollingInterval.Hour, rollOnFileSizeLimit: true)
                .CreateLogger();

            try
            {
                if (string.Equals(LogType, "INF"))
                {
                    Log.Information(Message, "[INF]");
                    Console.WriteLine($"[INF] - {Message}");
                }
                else if (string.Equals(LogType, "ERR"))
                {
                    Log.Error(Message, "[ERR]");
                    Console.WriteLine($"[ERR] - {Message}");
                }
            }
            catch (Exception exception)
            {
                if (string.Equals(LogType, "INF"))
                {
                    Log.Information(Message, "[INF]");
                    Console.WriteLine($"[INF] - {Message}");
                }
                else if (string.Equals(LogType, "ERR"))
                {
                    Log.Error(Message, "[ERR]");
                    Console.WriteLine($"[ERR] - {Message}");
                }
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}