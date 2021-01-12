namespace PH.LoggingExtensions.Nlog
{
    public class NlogFatalBuilder : NLogMessageBuilder
    {
        internal NlogFatalBuilder(NLog.ILogger logger,string className,string callerMemberName,string callerFilePath,int callerLineNumber) 
            : base(logger,className, callerMemberName, callerFilePath, callerLineNumber)
        {
            SetLevel(NLog.LogLevel.Fatal);
        }
    }
}