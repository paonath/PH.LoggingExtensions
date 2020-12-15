using Microsoft.Extensions.Logging;

namespace PH.LoggingExtensions
{
    public static class PhLoggingExtensions
    {
        
        public static TraceBuilderAppender Trace<TState>(this ILogger logger, TState scope, string message = "")
        {
            var t = new TraceBuilderAppender(logger, scope);
            if (!string.IsNullOrEmpty(message) && !string.IsNullOrWhiteSpace(message))
            {
                t.Trace(message);
            }

            return t;
        }
        
        public static TraceBuilderAppender Trace(this  ILogger logger,string message = "")
        {
            var t = new TraceBuilderAppender(logger);
            if (!string.IsNullOrEmpty(message) && !string.IsNullOrWhiteSpace(message))
            {
                t.Trace(message);
            }

            return t;
        }
    }
}