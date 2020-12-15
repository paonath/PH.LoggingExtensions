using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using Microsoft.Extensions.Logging;

namespace PH.LoggingExtensions
{
    public abstract class LogBuilderAppender : IDisposable
    {
        private StringBuilder _logBuilder;
        
        private SortedList<int,object> _objects;
        private readonly ILogger _logger;
        private int _paramCount;
        private Exception _settedException;
        private EventId? _settedEventId;
        private object _scope;
        
       
        protected internal LogBuilderAppender(LogLevel level, ILogger logger, object scope = null)
        {
            this.Level      = level;
            _logger         = logger;
            _paramCount      = 0;
            _settedEventId   = (EventId?) null;
            _settedException = null;
            if (null != scope)
            {
                _scope = scope;
            }
        }
        

        public LogLevel Level { get; }
        

        internal void AppendEvendId(EventId eventId)
        {
            if (_logger?.IsEnabled(Level) == true)
            {
                _settedEventId = eventId;
            }
        }

        internal void AppendScope(object scope)
        {
            if (_logger?.IsEnabled(Level) == true)
            {
                _scope = scope;
            }
        }

        internal void AppendException(Exception exception)
        {
            if (_logger?.IsEnabled(Level) == true)
            {
                _settedException = exception;
            }
        }
        internal void AppendMessage(string message)
        {
            if (_logger?.IsEnabled(Level) == true)
            {
                if (null == _logBuilder)
                {
                    _logBuilder = new StringBuilder();
                }

                if (!string.IsNullOrEmpty(message) && !string.IsNullOrWhiteSpace(message))
                {
                    message = message.TrimEnd();
                
                    if (_logBuilder.Length > 0 && !message.StartsWith(" ", StringComparison.InvariantCultureIgnoreCase))
                    {
                        message = " ";
                    }
                    _logBuilder.Append(message);
                }

                
            }
        }

        internal void AppendObject(object o)
        {
            AppendObjects(new[] {o});
        }

        internal void AppendObjects(params object[] objects)
        {
            if (_logger?.IsEnabled(Level) == true)
            {
                if (null == _objects)
                {
                    _objects = new SortedList<int, object>();
                }

                foreach (var o in objects)
                {
                    _objects.Add(_paramCount,o);
                    _paramCount++;
                }
            }
        }

        private void InternalFlushLog()
        {
            if (null == _logBuilder)
            {
                _logBuilder = new StringBuilder();
            }
            
            if (_objects?.Count > 0)
            {
                    
                var objs = _objects.OrderBy(x => x.Key).Select(x => x.Value).ToArray();
                if (_settedEventId.HasValue && null != _settedException)
                {
                    _logger.Log(Level,_settedEventId.Value,_settedException,_logBuilder.ToString(),objs);
                    return;
                }

                if (_settedEventId.HasValue && null == _settedException)
                {
                    _logger.Log(Level,_settedEventId.Value,_logBuilder.ToString(),objs);
                    return;
                }

                if (!_settedEventId.HasValue && null != _settedException)
                {
                    _logger.Log(Level,_settedException,_logBuilder.ToString(),objs);
                    return;
                }

                if (!_settedEventId.HasValue && null == _settedException)
                {
                    _logger.Log(Level,_logBuilder.ToString(), objs);
                    return;
                }
            }
            else
            {
                if (_settedEventId.HasValue && null != _settedException)
                {
                    _logger.Log(Level,_settedEventId.Value,_settedException,_logBuilder.ToString());
                    return;
                }

                if (_settedEventId.HasValue && null == _settedException)
                {
                    _logger.Log(Level,_settedEventId.Value,_logBuilder.ToString());
                    return;
                }

                if (!_settedEventId.HasValue && null != _settedException)
                {
                    _logger.Log(Level,_settedException,_logBuilder.ToString());
                    return;
                }

                if (!_settedEventId.HasValue && null == _settedException)
                {
                    _logger.Log(Level,_logBuilder.ToString());
                    
                }
            }
        }
        public void Flush()
        {
            if (_logger?.IsEnabled(Level) == true)
            {
                if (null != _scope)
                {
                    using (var scope = _logger.BeginScope(_scope))
                    {
                        InternalFlushLog();
                        
                    }
                }
                else
                {
                    InternalFlushLog();
                }
                
                

            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _objects?.Clear();
                _logBuilder = null;
                _settedException   = null;
                _scope      = null;
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