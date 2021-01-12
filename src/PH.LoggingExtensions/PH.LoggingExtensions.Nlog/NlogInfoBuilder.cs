namespace PH.LoggingExtensions.Nlog
{
    public class NlogInfoBuilder : NLogMessageBuilder
    {
        

        internal NlogInfoBuilder(NLog.ILogger logger,string className,string callerMemberName,string callerFilePath,int callerLineNumber) 
            : base(logger,className, callerMemberName, callerFilePath, callerLineNumber)
        {
            SetLevel(NLog.LogLevel.Info);
        }
    }
}