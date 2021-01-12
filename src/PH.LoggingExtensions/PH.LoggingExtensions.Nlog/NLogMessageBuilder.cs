using NLog;

namespace PH.LoggingExtensions.Nlog
{
    public class NLogMessageBuilder : GenericMessageBuilder
    {
        internal NLog.LogLevel _level;
        internal readonly NLog.ILogger _logger;
        internal LogEventInfo _evtInfo;
        internal NLogMessageBuilder(NLog.ILogger logger,string className,string callerMemberName,string callerFilePath,int callerLineNumber)
            :base()
        {
            _logger = logger;
            _level  = LogLevel.Off;
            _evtInfo = new LogEventInfo()
            {
                Level = NLog.LogLevel.Off
            };
            _evtInfo.SetCallerInfo(className, callerMemberName, callerFilePath, callerLineNumber);
        }

        protected internal void SetLevel(NLog.LogLevel level)
        {
            _evtInfo.Level = level;
            _level         = level;
        }

        public void Flush()
        {
            if (null != _logger)
            {
                if (!Empty && _logger.IsEnabled(_level))
                {
                    var data = PrepareMessage();
                    _evtInfo.Message    = data.Message;
                    _evtInfo.Parameters = data.paramters;
                    _evtInfo.Exception  = data.exception;
                    _logger.Log(_evtInfo);
                }
            }
            Reset();
        }
    }
}