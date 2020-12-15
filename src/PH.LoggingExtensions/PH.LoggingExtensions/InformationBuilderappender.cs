using System;
using Microsoft.Extensions.Logging;

namespace PH.LoggingExtensions
{
    public class InformationBuilderappender : LogBuilderAppender
    {
        internal InformationBuilderappender(ILogger logger, object scope = null) : base(LogLevel.Information, logger, scope)
        {
        }
        
        public InformationBuilderappender AddScope(object scope)
        {
            AppendScope(scope);
            return this;
        }

        public InformationBuilderappender Information(string messagePart)
        {
            base.AppendMessage(messagePart);
            return this;
        }

        public InformationBuilderappender Information(string messagePart, object param)
        {
            AppendMessage(messagePart);
            AppendObjects(param);
            return this;
        }

        public InformationBuilderappender EventId(EventId eventId)
        {
            AppendEvendId(eventId);
            return this;
        }

        public InformationBuilderappender Information(EventId eventId) => EventId(eventId);

        public InformationBuilderappender Exception(Exception exception)
        {
            AppendException(exception);
            return this;
        }

        public InformationBuilderappender Information(Exception exception) => Exception(exception);
        
        
        public InformationBuilderappender Information(object param)
        {
            AppendObjects(param);
            return this;
        }


    }
}