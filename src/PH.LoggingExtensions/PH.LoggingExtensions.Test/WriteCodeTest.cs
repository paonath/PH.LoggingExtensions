using System;
using System.Collections.Concurrent;
using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Xunit;
using Xunit.Abstractions;
using ILogger = Microsoft.Extensions.Logging.ILogger;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace PH.LoggingExtensions.Test
{
    public class FakeLogger : ILogger
    {
        private readonly LogLevel _minLevel;
        private readonly string _name;
        private readonly ITestOutputHelper _helper;
        public FakeLogger(LogLevel minLevel, string name, ITestOutputHelper helper)
        {
            _minLevel    = minLevel;
            _name        = name;
            _helper = helper;
        }


        /// <summary>Writes a log entry.</summary>
        /// <param name="logLevel">Entry will be written on this level.</param>
        /// <param name="eventId">Id of the event.</param>
        /// <param name="state">The entry to be written. Can be also an object.</param>
        /// <param name="exception">The exception related to this entry.</param>
        /// <param name="formatter">Function to create a <see cref="T:System.String" /> message of the <paramref name="state" /> and <paramref name="exception" />.</param>
        /// <typeparam name="TState">The type of the object to be written.</typeparam>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            
            
                _helper.WriteLine($"[{eventId.Id,2}: {logLevel,-12}]");
                _helper.WriteLine($"     {_name} - {formatter(state, exception)}");
          

        }

        /// <summary>
        /// Checks if the given <paramref name="logLevel" /> is enabled.
        /// </summary>
        /// <param name="logLevel">level to be checked.</param>
        /// <returns><c>true</c> if enabled.</returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            return ((int) logLevel) >= ((int) _minLevel);
        }

        /// <summary>Begins a logical operation scope.</summary>
        /// <param name="state">The identifier for the scope.</param>
        /// <typeparam name="TState">The type of the state to begin scope for.</typeparam>
        /// <returns>An <see cref="T:System.IDisposable" /> that ends the logical operation scope on dispose.</returns>
        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }
    }

    public sealed class ColorConsoleLoggerProvider : ILoggerProvider
    {
        private readonly ITestOutputHelper _helper;
        private readonly ConcurrentDictionary<string, FakeLogger> _loggers =
            new ConcurrentDictionary<string, FakeLogger>();

        public ColorConsoleLoggerProvider(ITestOutputHelper helper)
        {
            _helper = helper;
        }

      

        public ILogger CreateLogger(string categoryName) =>
            _loggers.GetOrAdd(categoryName, name => new FakeLogger(LogLevel.Trace, name,_helper));

        public void Dispose() => _loggers.Clear();
    }

    public class Foo
    {
        public Guid Guid { get; }
        public DateTime UcNow { get; }

        public Foo()
        {
            Guid = Guid.NewGuid();
            UcNow = DateTime.UtcNow;
        }
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


            var builder = new LogMessageBuilder();
            builder.Message("pippo");
            //builder.Flush();
            builder.FlushTo(logger);



            logger.LogDebug("simply debug");
            var foo  = new Foo();
            Foo foo2 = null;

            var prov = new ColorConsoleLoggerProvider(Output);

            var myLogger = prov.CreateLogger("dummy");
            myLogger.LogDebug("a debug on my fake");

            try
            {
                var u = foo2.Guid;
            }
            catch (Exception e)
            {
                myLogger.LogError("dummy", e);
            }

            using (logger.DebugRegion("a foo json {@jj}", foo))
            {
                logger.LogInformation("soma info");
            }
          
        }
    }

  
}