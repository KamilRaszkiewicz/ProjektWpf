using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TestsGenerator.App
{
    public static class LoggerExtensions
    {
        private const string FORMAT = "Exception caught in {className}, method {methodName}. Exception: {exception}";

        public static void LogException<T>(this ILogger<T> logger, string className, string methodName, Exception e, LogLevel logLevel = LogLevel.Error)
        {
            logger.Log(logLevel, FORMAT, className, methodName, e);
        }
    }
}
