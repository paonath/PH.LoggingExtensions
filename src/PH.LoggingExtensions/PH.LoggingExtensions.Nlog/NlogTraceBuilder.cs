namespace PH.LoggingExtensions.Nlog
{
    public class NlogTraceBuilder : NLogMessageBuilder
    {
        internal NlogTraceBuilder(NLog.ILogger logger,string className,string callerMemberName,string callerFilePath,int callerLineNumber) 
            : base(logger,className, callerMemberName, callerFilePath, callerLineNumber)
        {
            SetLevel(NLog.LogLevel.Trace);
        }
    }
}