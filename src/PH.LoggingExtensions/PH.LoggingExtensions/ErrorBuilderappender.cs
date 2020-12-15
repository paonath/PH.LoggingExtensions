using System;
using Microsoft.Extensions.Logging;

namespace PH.LoggingExtensions
{
    public class ErrorBuilderappender : LogBuilderAppender
    {
        internal ErrorBuilderappender(ILogger logger, object scope = null) : base(LogLevel.Error, logger, scope)
        {
        }
        
        public ErrorBuilderappender AddScope(object scope)
        {
            AppendScope(scope);
            return this;
        }

        public ErrorBuilderappender Error(string messagePart)
        {
            base.AppendMessage(messagePart);
            return this;
        }

        public ErrorBuilderappender Error(string messagePart, object param)
        {
            AppendMessage(messagePart);
            AppendObjects(param);
            return this;
        }

        public ErrorBuilderappender EventId(EventId eventId)
        {
            AppendEvendId(eventId);
            return this;
        }

        public ErrorBuilderappender Error(EventId eventId) => EventId(eventId);

        public ErrorBuilderappender Exception(Exception exception)
        {
            AppendException(exception);
            return this;
        }

        public ErrorBuilderappender Error(Exception exception) => Exception(exception);
        
        
        public ErrorBuilderappender Error(object param)
        {
            AppendObjects(param);
            return this;
        }

    }
}