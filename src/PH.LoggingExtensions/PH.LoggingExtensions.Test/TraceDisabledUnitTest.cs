using System;
using Autofac;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace PH.LoggingExtensions.Test
{
    public class TraceDisabledUnitTest : BaseUnitTest
    {
        [Fact]
        public void TraceTest()
        {
            var logger = Scope.Resolve<ILogger<TraceUnitTest>>();
            
            
            
            logger.Trace()
                  .Trace("some data at {now}")
                  .Trace(DateTime.Now)
                  .Flush();
        }

        
        public TraceDisabledUnitTest(ITestOutputHelper output) : base(output, LogLevel.Warning)
        {
        }
    }
}
