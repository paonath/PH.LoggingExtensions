using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace PH.LoggingExtensions
{
    

    public static class LogExtensions
    {



        public static IDisposable TraceRegion(this ILogger logger, string region, params object[] otObjects)
        {
            return new DisposableLogRegion(logger, LogLevel.Trace, region, otObjects);
        }  
        public static IDisposable DebugRegion(this ILogger logger, string region, params object[] otObjects)
        {
            return new DisposableLogRegion(logger, LogLevel.Debug, region, otObjects);
        }

        public static IDisposable InformationRegion(this ILogger logger, string region, params object[] otObjects)
        {
            return new DisposableLogRegion(logger, LogLevel.Information, region, otObjects);
        } 
        
        public static IDisposable WarningRegion(this ILogger logger, string region, params object[] otObjects)
        {
            return new DisposableLogRegion(logger, LogLevel.Warning, region, otObjects);
        }

        public static IDisposable CriticalRegion(this ILogger logger, string region, params object[] otObjects)
        {
            return new DisposableLogRegion(logger, LogLevel.Critical, region, otObjects);
        }

        public static IDisposable ErrorRegion(this ILogger logger, string region, params object[] otObjects)
        {
            return new DisposableLogRegion(logger, LogLevel.Error, region, otObjects);
        }

    }

    public class DisposableLogRegion : IDisposable
    {
        private readonly ILogger _logger;
        private IDisposable _scope;
        private LogLevel _level;
        private string _region;
        private object[] _parameters;

        internal DisposableLogRegion(ILogger logger, LogLevel logLevel, string region, params object[] otObjects)
        {

            _logger     = logger;
            _level      = logLevel;
            _region     = region;
            _parameters = otObjects;
            _scope      = _logger.BeginScope(region);
            ComposeBegin();
        }

        private void ComposeBegin()
        {
            _logger.Log(_level, "----> {regionName} [BEGIN]", _region, _parameters);
        }

        private void ComposeEnd()
        {
            _logger.Log(_level, "<---- {regionName} [END]", _region, _parameters);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                ComposeEnd();
                _scope?.Dispose();
            }
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
