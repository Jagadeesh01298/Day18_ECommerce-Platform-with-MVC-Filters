namespace ECommerceMvcFilters.Services
{
    public class FileAppLogger : IAppLogger
    {
        private readonly IWebHostEnvironment _environment;

        public FileAppLogger(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public void LogInfo(string message)
        {
            WriteLog("INFO", message);
        }

        public void LogError(string message, Exception exception)
        {
            string fullMessage = $"{message} | Exception: {exception.Message}";
            WriteLog("ERROR", fullMessage);
        }

        private void WriteLog(string level, string message)
        {
            string logFolder = Path.Combine(_environment.ContentRootPath, "Logs");

            if (!Directory.Exists(logFolder))
            {
                Directory.CreateDirectory(logFolder);
            }

            string logFile = Path.Combine(logFolder, "app-log.txt");

            string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] {message}{Environment.NewLine}";

            File.AppendAllText(logFile, logMessage);
        }
    }
}
