using System;
using System.Security.Claims;
using System.Threading;
using Autofac;
using Microsoft.Extensions.Logging;
using NLog;
using Xunit;
using Xunit.Abstractions;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace PH.LoggingExtensions.Test
{
    public static class TraceLogMessageExtensions
    {
        private static Func<Microsoft.Extensions.Logging.ILogger, DateTime, IDisposable> _processingWorkScope;
        
        public static IDisposable ProcessingWorkScope(
            this Microsoft.Extensions.Logging.ILogger logger, DateTime time) =>
            _processingWorkScope(logger, time);
    }
    public class TraceLogMessage
    {
        
        
        
    }

    public class TraceUnitTest : BaseUnitTest
    {
        [Fact]
        public void TraceTest()
        {
            var logger = Scope.Resolve<ILogger<TraceUnitTest>>();


            logger.LogTrace("a prev trace at {now}", DateTime.Now);
            
            logger.PippoLog("sto cazzo {id}", Guid.NewGuid());


            new TraceBuilder()
                .Trace("a param date {dt}")
                .Trace(DateTime.Now)
                .Trace("another")
                .Trace("a param guid {gd}")
                .Trace(Guid.NewGuid())
                .Trace("another msg")
                .Flush(logger);
            
            
              
            
           
            
                                 //.FlushTo(logger);
            
            
            
            
            logger.LogTrace("a default trace");
            
            

            ObjectClass obj = new ObjectClass() {DateTime = DateTime.UtcNow, Message = "a message"};
            
            logger.Trace()
                  .Trace("some data at {now}")
                  .Trace(DateTime.Now)
                  
                  .AddScope("testing data")
                  .Trace(new EventId(1,"a fake eventId"))
                  .Flush();
        }

        
        public TraceUnitTest(ITestOutputHelper output) : base(output, LogLevel.Trace)
        {
        }
    }


    public class ObjectClass
    {
        public DateTime DateTime { get; set; }
        public Object Obj { get; set; }
        public string Message { get; set; }
    }
}
