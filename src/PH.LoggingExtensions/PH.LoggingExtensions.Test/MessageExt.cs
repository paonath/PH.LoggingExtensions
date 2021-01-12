using System;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using NLog;
using ILogger = Microsoft.Extensions.Logging.ILogger;
using LogLevel = NLog.LogLevel;

namespace PH.LoggingExtensions.Test
{
    

    public static class MessageExt
    {
        public static void FlushTo(this LogMessageBuilder builder ,ILogger instance, [CallerMemberName] string callerMemberName = null,
                                   [CallerFilePath] string callerFilePath = null,
                                   [CallerLineNumber] int callerLineNumber = 0)
        {
            var stackTrace = new System.Diagnostics.StackTrace(1); // skip one frame as this is the Log function frame
            var name       = stackTrace.GetFrame(0).GetMethod().Name;

            var logData    = builder.GetMessage();
            
            instance.LogInformation(logData.MessageLog, logData.ParamObjects);
        }

        private static NLog.LogLevel getNLogLevel(Microsoft.Extensions.Logging.LogLevel level)
        {
            switch (level)
            {
                case Microsoft.Extensions.Logging.LogLevel.Trace:
                    return NLog.LogLevel.Trace;
                case Microsoft.Extensions.Logging.LogLevel.Debug:
                    return NLog.LogLevel.Debug;
                case Microsoft.Extensions.Logging.LogLevel.Information:
                    return NLog.LogLevel.Info;
                case Microsoft.Extensions.Logging.LogLevel.Warning:
                    return NLog.LogLevel.Warn;
                case Microsoft.Extensions.Logging.LogLevel.Error:
                    return NLog.LogLevel.Error;
                case Microsoft.Extensions.Logging.LogLevel.Critical:
                    return NLog.LogLevel.Fatal;
                case Microsoft.Extensions.Logging.LogLevel.None:
                    return NLog.LogLevel.Off;
                default:
                    return NLog.LogLevel.Trace;
                    
            }


        }

        public static void FlushToNlog(this LogMessageBuilder builder, NLog.ILogger logger, [CallerMemberName] string callerMemberName = null,
                                       [CallerFilePath] string callerFilePath = null,
                                       [CallerLineNumber] int callerLineNumber = 0)
        {
            string methodName = "";
            string className = "";
            var stackTrace = new System.Diagnostics.StackTrace(1); // skip one frame as this is the Log function frame
            var method     = stackTrace.GetFrame(0)?.GetMethod();
            if (null != method)
            {
                className = method?.DeclaringType?.Name;
            }
            
            var logData = builder.GetMessage();
            var evtInfo = new LogEventInfo();
            evtInfo.Message    = logData.MessageLog;
            evtInfo.Parameters = logData.ParamObjects;
            evtInfo.Level      = getNLogLevel(logData.LogLevel);

            evtInfo.SetCallerInfo(className, callerMemberName, callerFilePath, callerLineNumber);

            logger.Log(evtInfo);
        }
    }
}