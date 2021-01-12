using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Extensions.Logging;

namespace PH.LoggingExtensions
{
    /// <summary>
    /// Microsoft.Extensions.Logging Extensions class 
    /// </summary>
    public static class PhLogExtensions
    {
        /// <summary>Begins the named object scope.</summary>
        /// <param name="logger">The logger.</param>
        /// <param name="scopeName">Name of the scope.</param>
        /// <param name="objectParam">The object parameter.</param>
        /// <returns>IDisposable scope</returns>
        public static IDisposable BeginNamedObjectScope(this ILogger logger, string scopeName, object objectParam)
            => logger.BeginScope(new KeyValuePair<string, object>(scopeName, objectParam));
        

        /// <summary>Begins the source scope context.</summary>
        /// <param name="logger">The logger.</param>
        /// <param name="callerMemberName">Name of the caller member.</param>
        /// <param name="callerFilePath">The caller file path.</param>
        /// <param name="callerLineNumber">The caller line number.</param>
        /// <returns>IDisposable scope</returns>
        public static IDisposable BeginSourceScopeContext(this Microsoft.Extensions.Logging.ILogger logger,
                                                          [CallerMemberName] string callerMemberName = null,
                                                          [CallerFilePath] string callerFilePath = null,
                                                          [CallerLineNumber] int callerLineNumber = 0)
        {

            var sourceLogState = new
            {
                CallerMemberName = callerMemberName,
                CallerFilePath   = callerFilePath,
                CallerLineNumber = callerLineNumber
            };
            return logger.BeginScope(sourceLogState);
        }

        /// <summary>Begins the source scope context.</summary>
        /// <typeparam name="TState">The type of the state.</typeparam>
        /// <param name="logger">The logger.</param>
        /// <param name="state">The state.</param>
        /// <param name="callerMemberName">Name of the caller member.</param>
        /// <param name="callerFilePath">The caller file path.</param>
        /// <param name="callerLineNumber">The caller line number.</param>
        /// <returns>IDisposable scope</returns>
        public static IDisposable BeginSourceScopeContext<TState>(this Microsoft.Extensions.Logging.ILogger logger,
                                                                  TState state,
                                                                  [CallerMemberName] string callerMemberName = null,
                                                                  [CallerFilePath] string callerFilePath = null,
                                                                  [CallerLineNumber] int callerLineNumber = 0)
        {

            var scp = new
            {
                State = state,
                SourceLogState = new
                {
                    CallerMemberName = callerMemberName,
                    CallerFilePath   = callerFilePath,
                    CallerLineNumber = callerLineNumber
                }
            };
            return logger.BeginScope(scp);
        }
    }
}