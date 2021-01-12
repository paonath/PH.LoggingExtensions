namespace PH.LoggingExtensions.Nlog
{
    public class NlogErrorBuilder : NLogMessageBuilder
    {
        internal NlogErrorBuilder(NLog.ILogger logger,string className,string callerMemberName,string callerFilePath,int callerLineNumber) 
            : base(logger,className, callerMemberName, callerFilePath, callerLineNumber)
        {
            SetLevel(NLog.LogLevel.Error);
        }
    }
}