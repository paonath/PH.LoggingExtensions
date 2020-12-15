using System;
using Microsoft.Extensions.Logging;

namespace PH.LoggingExtensions
{
    public class CriticalBuilderAppender : LogBuilderAppender
    {
        internal CriticalBuilderAppender(ILogger logger, object scope = null) : base(LogLevel.Critical, logger, scope)
        {
        }
        public CriticalBuilderAppender AddScope(object scope)
        {
            AppendScope(scope);
            return this;
        }

        public CriticalBuilderAppender Critical(string messagePart)
        {
            base.AppendMessage(messagePart);
            return this;
        }

        public CriticalBuilderAppender Critical(string messagePart, object param)
        {
            AppendMessage(messagePart);
            AppendObjects(param);
            return this;
        }

        public CriticalBuilderAppender EventId(EventId eventId)
        {
            AppendEvendId(eventId);
            return this;
        }

        public CriticalBuilderAppender Critical(EventId eventId) => EventId(eventId);

        public CriticalBuilderAppender Exception(Exception exception)
        {
            AppendException(exception);
            return this;
        }

        public CriticalBuilderAppender Critical(Exception exception) => Exception(exception);
        
        
        public CriticalBuilderAppender Critical(object param)
        {
            AppendObjects(param);
            return this;
        }
    }
}