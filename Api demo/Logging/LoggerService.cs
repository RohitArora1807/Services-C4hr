
namespace Api_demo.Logging
{
    public class LoggerService
    {
        private readonly string _logFilePath;

        public LoggerService(string logDirectory)
        {
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            _logFilePath = Path.Combine(logDirectory, "errors.log");
        }

        public void LogError(string message, Exception ex = null)
        {
            try
            {
                var logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | ERROR | {message}";

                if (ex != null)
                {
                    logMessage += $"{Environment.NewLine}Exception: {ex.Message}{Environment.NewLine}Stack Trace: {ex.StackTrace}";
                }

                File.AppendAllText(_logFilePath, logMessage + Environment.NewLine);
            }
            catch (Exception logEx)
            {
                Console.WriteLine($"Failed to log error: {logEx.Message}");
            }
        }
    }
}
