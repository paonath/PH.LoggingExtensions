using System;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace PH.LoggingExtensions
{
    public static class TraceBuilderExt
    {
        //public static void Flush(this TraceBuilder builder, ILogger logger) =>
        //    logger.Log(LogLevel.Trace, builder._settedEventId, builder._logBuilder.ToString(), builder._settedException,
        //               builder._objects.OrderBy(x => x.Key).Select(x => x.Value).ToArray());

        //public static void Flush(this TraceBuilder builder, ILogger logger) => () =>
        //    logger.Log(LogLevel.Trace, builder._settedEventId, builder._logBuilder.ToString(), builder._settedException,
        //               builder._objects.OrderBy(x => x.Key).Select(x => x.Value).ToArray());


        public static void PippoLog(this ILogger logger, string message, params object[] objects) => logger.Log(LogLevel.Critical, message, objects);
        
        //private static Action<TraceBuilder, ILogger, EventId,string,Exception,object[]> logAction =
        //    (builder, logger, arg3, arg4, arg5, arg6) => logger.Log(LogLevel.Trace, arg3, arg4, arg5, arg6);
            
            //logger.Log(LogLevel.Trace, builder._settedEventId, builder._logBuilder.ToString(), builder._settedException,
            //           builder._objects.OrderBy(x => x.Key).Select(x => x.Value).ToArray());



            //public static void Flush(this TraceBuilder builder, ILogger logger) => logAction(builder, logger,
            // builder._settedEventId, builder._logBuilder.ToString(), builder._settedException,
            // builder._objects.OrderBy(x => x.Key).Select(x => x.Value).ToArray());
            


            //=> builder.FlushAction.Invoke(logger, LogLevel.Trace, builder._settedEventId,
            //                              builder._logBuilder.ToString(), builder._settedException,
            //                              builder._objects.OrderBy(x => x.Key).Select(x => x.Value).ToArray());
            //private static void PeformFlush(TraceBuilder builder)
    }
}