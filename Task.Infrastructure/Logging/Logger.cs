using System;
using NLog;

namespace Task.Infrastructure.Logging
{
    public class Logger
    {
        /// <summary>
        /// Logger with name "Task" which uses configuration from NLog.congif
        /// </summary>
        private static readonly NLog.Logger logger = LogManager.GetLogger("Task");

        /// <summary>
        /// Logs message
        /// </summary>
        /// <param name="message"></param>
        public static void Log(string message)
        {
            logger.Log(LogLevel.Info, message);
        }

        /// <summary>
        /// Logs exception
        /// </summary>
        /// <param name="exception"></param>
        public static void Log(Exception exception)
        {
            logger.Log(LogLevel.Error, exception.Message + exception.InnerException + exception.StackTrace);
        }
    }
}