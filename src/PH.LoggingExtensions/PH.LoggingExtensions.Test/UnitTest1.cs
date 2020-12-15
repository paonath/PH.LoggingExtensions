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

    
    public class TraceUnitTest : BaseUnitTest
    {
        [Fact]
        public void TraceTest()
        {
            var logger = Scope.Resolve<ILogger<TraceUnitTest>>();

            var wr = new Wr(logger);
            wr.Flush();
            
            
            logger.LogTrace("a default trace");
            
            logger.LogTrace("a prev trace at {now}", DateTime.Now);

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
