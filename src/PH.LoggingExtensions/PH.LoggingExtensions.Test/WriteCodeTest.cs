using System;
using Autofac;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace PH.LoggingExtensions.Test
{
    class PippoDummy
    {
        public PippoDummy(string name)
        {
            Name = name;
            Date = DateTime.UtcNow;
        }
        public DateTime Date { get; }
        public string Name { get; set; }
    }
    public class WriteCodeTest : BaseUnitTest
    {
        public WriteCodeTest(ITestOutputHelper output) : base(output, LogLevel.Trace)
        {
        }

        [Fact]
        public void WriteTest()
        {
            var logger = Scope.Resolve<ILogger<WriteCodeTest>>();
            logger.LogDebug("simply debug");

            logger.LogDebug("a value for dummy {@dummy}", new PippoDummy("paolo"));

            logger.Log(LogLevel.Critical,"pippo");


            var info2 = logger.InfoB()
                              .Info("message")
                              .Info("another");

            info2.Flush();
                              
            
            
            
            
            
            var info = logger.Info("pippo")
                  .Info(" pluto")
                  .Info(" e paperino");
                
                
                
                  info.Flush();
            
        }
    }

    public static class DefeExt
    {
        public static Defer Info(this ILogger logger, string message = "")
        {
            return new Defer(logger, message);
        }

        public static void Flush(this Defer defer)
        {
            defer._flushLog.Invoke();
        }
    }
}