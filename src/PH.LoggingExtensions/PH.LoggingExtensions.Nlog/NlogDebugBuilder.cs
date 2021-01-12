﻿namespace PH.LoggingExtensions.Nlog
{
    public class NlogDebugBuilder : NLogMessageBuilder
    {
        internal NlogDebugBuilder(NLog.ILogger logger,string className,string callerMemberName,string callerFilePath,int callerLineNumber) 
            : base(logger,className, callerMemberName, callerFilePath, callerLineNumber)
        {
            SetLevel(NLog.LogLevel.Debug);
        }
    }
}