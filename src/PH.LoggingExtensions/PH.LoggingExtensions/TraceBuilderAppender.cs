using System;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace PH.LoggingExtensions
{
    /*

        Error = 4,
        Critical = 5,
        None = 6
     */
    
    public class TraceBuilderAppender : LogBuilderAppender
    {
        internal TraceBuilderAppender(ILogger logger, object scope = null) : base(LogLevel.Trace, logger, scope)
        {
        }

        public TraceBuilderAppender AddScope(object scope)
        {
            AppendScope(scope);
            return this;
        }

        public TraceBuilderAppender Trace(string messagePart)
        {
            base.AppendMessage(messagePart);
            return this;
        }

        public TraceBuilderAppender Trace(string messagePart, object param)
        {
            AppendMessage(messagePart);
            AppendObjects(param);
            return this;
        }

        public TraceBuilderAppender EventId(EventId eventId)
        {
            AppendEvendId(eventId);
            return this;
        }

        public TraceBuilderAppender Trace(EventId eventId) => EventId(eventId);

        public TraceBuilderAppender Exception(Exception exception)
        {
            AppendException(exception);
            return this;
        }

        public TraceBuilderAppender Trace(Exception exception) => Exception(exception);
        
        
        public TraceBuilderAppender Trace(object param)
        {
            AppendObjects(param);
            return this;
        }
        
        
    }

    public class DebugBuilderAppender : LogBuilderAppender
    {
        internal DebugBuilderAppender(ILogger logger, object scope = null) 
            : base(LogLevel.Debug, logger, scope)
        {
        }
        
        public DebugBuilderAppender AddScope(object scope)
        {
            AppendScope(scope);
            return this;
        }

        public DebugBuilderAppender Debug(string messagePart)
        {
            base.AppendMessage(messagePart);
            return this;
        }

        public DebugBuilderAppender Debug(string messagePart, object param)
        {
            AppendMessage(messagePart);
            AppendObjects(param);
            return this;
        }

        public DebugBuilderAppender EventId(EventId eventId)
        {
            AppendEvendId(eventId);
            return this;
        }

        public DebugBuilderAppender Debug(EventId eventId) => EventId(eventId);

        public DebugBuilderAppender Exception(Exception exception)
        {
            AppendException(exception);
            return this;
        }

        public DebugBuilderAppender Debug(Exception exception) => Exception(exception);
        
        
        public DebugBuilderAppender Debug(object param)
        {
            AppendObjects(param);
            return this;
        }


    }

    
}
