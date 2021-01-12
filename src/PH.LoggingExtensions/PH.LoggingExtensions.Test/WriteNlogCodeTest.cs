using System;
using NLog;
using NLog.Fluent;
using PH.LoggingExtensions.Nlog;
using Xunit;
using Xunit.Abstractions;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace PH.LoggingExtensions.Test
{
    public class WriteNlogCodeTest : BaseUnitTest
    {
        public WriteNlogCodeTest(ITestOutputHelper output) : base(output, LogLevel.Trace)
        {
        }

        [Fact]
        public void Fake()
        {
            //nLogger.Log(new LogEventInfo(NLog.LogLevel.Debug, "some logger","some message {date} {@json}" ));
            
            var builder = new LogMessageBuilder();
            builder.Message("pippo")
                   .Message("A message {date} and class {@json}", DateTime.UtcNow, new Foo());

            
            builder.FlushToNlog(nLogger);

            //nLogger.Fluent()

            nLogger.Fluent()
                   .Info("second message")
                   .Info("message with Guid {guid}", Guid.NewGuid())
                   .Info("class info {@info}", new Foo())
                   .Flush();


            //nLogger.

            nLogger.Fluent(NLog.LogLevel.Info)
                   .Info("info message")
                   .Info("another info")
                   .Flush();


            nLogger.Fluent().Trace()
                   .Trace("TRACE message {UID}", Guid.NewGuid())
                   .Trace()
                   .Trace("another info")
                   .Flush();

            var nullLogger = NLog.LogManager.CreateNullLogger();
            nullLogger.Fluent(NLog.LogLevel.Info).Info("a message").Flush();

            //nLogger.Fluent(NLog.LogLevel.Info).

        }

        [Fact]
        public void TestingFluentNLog()
        {
            /*
            nLogger.Info()
                   .Message("Starting message")
                   .Message("another message with data: {uid} {@json}", Guid.NewGuid(), new Foo())
                   .Write();*/

        }
    }
}