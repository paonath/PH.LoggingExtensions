namespace PH.LoggingExtensions.Nlog
{
    public class NlogWarnBuilder : NLogMessageBuilder
    {
        internal NlogWarnBuilder(NLog.ILogger logger,string className,string callerMemberName,string callerFilePath,int callerLineNumber) 
            : base(logger,className, callerMemberName, callerFilePath, callerLineNumber)
        {
            SetLevel(NLog.LogLevel.Warn);
        }
    }
}