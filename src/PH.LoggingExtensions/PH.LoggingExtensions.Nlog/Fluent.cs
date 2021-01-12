using System;
using NLog;

namespace PH.LoggingExtensions.Nlog
{
    public class Fluent
    {
        internal NLogMessageBuilder MessageBuilder { get; }
        internal Fluent(NLog.ILogger logger,string className,string callerMemberName,string callerFilePath,int callerLineNumber)
            : this(LogLevel.Trace, logger, className, callerMemberName, callerFilePath, callerLineNumber)
        {
            MessageBuilder = new NlogTraceBuilder(logger, className, callerMemberName, callerFilePath,
                                                  callerLineNumber);
        }

        internal Fluent(LogLevel nlevel, NLog.ILogger logger, string className, string callerMemberName,
                        string callerFilePath, int callerLineNumber)
        {
            if (nlevel == LogLevel.Off)
            {
                throw new ArgumentException($"Unable to create a fluent class for Off level!", nameof(nlevel));
            }
            if (nlevel == LogLevel.Info)
            {
                MessageBuilder = new NlogInfoBuilder(logger, className, callerMemberName, callerFilePath,
                                                     callerLineNumber);
            }
            else
            {
                if (nlevel == LogLevel.Trace)
                {
                    MessageBuilder = new NlogTraceBuilder(logger, className, callerMemberName, callerFilePath,
                                                          callerLineNumber);
                }
                else
                {
                    if (nlevel == LogLevel.Debug)
                    {
                        MessageBuilder = new NlogDebugBuilder(logger, className, callerMemberName, callerFilePath,
                                                              callerLineNumber);
                    }
                    else
                    {
                        if (nlevel == LogLevel.Warn)
                        {
                            MessageBuilder = new NlogWarnBuilder(logger, className, callerMemberName, callerFilePath,
                                                                 callerLineNumber);
                        }
                        else
                        {
                            if (nlevel == LogLevel.Error)
                            {
                                MessageBuilder = new NlogErrorBuilder(logger, className, callerMemberName, callerFilePath,
                                                                      callerLineNumber);
                            }


                            MessageBuilder = new NlogFatalBuilder(logger, className, callerMemberName, callerFilePath,
                                                                  callerLineNumber);

                        }
                    }
                }
            }


        }
    }
}