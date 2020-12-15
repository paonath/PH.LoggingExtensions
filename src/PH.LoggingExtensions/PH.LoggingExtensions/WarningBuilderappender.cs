using System;
using Microsoft.Extensions.Logging;

namespace PH.LoggingExtensions
{
    public class WarningBuilderappender : LogBuilderAppender
    {
        internal WarningBuilderappender(ILogger logger, object scope = null) : base(LogLevel.Warning, logger, scope)
        {
        }
        
        public WarningBuilderappender AddScope(object scope)
        {
            AppendScope(scope);
            return this;
        }

        public WarningBuilderappender Warning(string messagePart)
        {
            base.AppendMessage(messagePart);
            return this;
        }

        public WarningBuilderappender Warning(string messagePart, object param)
        {
            AppendMessage(messagePart);
            AppendObjects(param);
            return this;
        }

        public WarningBuilderappender EventId(EventId eventId)
        {
            AppendEvendId(eventId);
            return this;
        }

        public WarningBuilderappender Warning(EventId eventId) => EventId(eventId);

        public WarningBuilderappender Exception(Exception exception)
        {
            AppendException(exception);
            return this;
        }

        public WarningBuilderappender Warning(Exception exception) => Exception(exception);
        
        
        public WarningBuilderappender Warning(object param)
        {
            AppendObjects(param);
            return this;
        }


    }
}