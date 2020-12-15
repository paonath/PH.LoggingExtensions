using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Extensions.Logging;

namespace PH.LoggingExtensions
{

    //public class Tb
    //{
    //    private ILogger _logger;
    //    private string _m;
    //    private object[] _objects;
    //    //FlushDelegate flushDelegate = (logger, level, id, message, exception, objParams) =>
    //    //    logger.Log(level, id, message, exception, objParams);

    //    private FlushDelegate _del = logger => logger.Log(LogLevel.Debug, _m, _objects); 
        
    //    public Tb(ILogger logger)
    //    {
    //        _logger = logger;
            

            
    //    }

    //    public Tb Trace(string m)
    //    {
    //        _m = m;
    //        return this;
    //    }
    //    public Tb Trace(object[] objs)
    //    {
    //        _objects = objs;
    //        return this;
    //    }

    //    public void Flush() => flushDelegate.Invoke(_logger);

    //    //public delegate void FlushDelegate(ILogger logger,LogLevel level, EventId eventId, string message, Exception exception,
    //    //                           params object[] objParams);
    //    public delegate void FlushDelegate(ILogger logger);

    //}

    //public static class My
    //{
    //    internal static string _m;
    //    internal static object[] _o;
        
        
    //    public static My Trace(this ILogger logger, string message)
    //    {
    //        _m = message;
    //        return logger;
    //    }
    //}

    public class TraceBuilder : MessageBuilder
    {
        public TraceBuilder Trace(string message)
        {
            AppendMessage(message);
            return this;
        }

        public TraceBuilder Trace(object objectParam)
        {
            AppendObject(objectParam);
            return this;
        }

        public void Flush(ILogger logger)
        {
            StackTrace   st       = new StackTrace(1, true);
            StackFrame[] stFrames = st.GetFrames();

            
            var        msg        = _logBuilder.ToString();
            var        paramsObjs = _objects.OrderBy(x => x.Key).Select(x => x.Value).ToArray();
            logger.Log(LogLevel.Debug, _settedEventId, _settedException, msg, paramsObjs);
            
        }
        

        //public delegate void Flush(LogLevel level, EventId eventId, string message, Exception exception,
        //                           params object[] objParams);

        //public Action FlushAct(ILogger logger) => () =>
        //    logger.Log(LogLevel.Critical, _settedEventId, _logBuilder.ToString(), _settedException,
        //               _objects.OrderBy(x => x.Key).Select(x => x.Value).ToArray());


        //public static void FlushTo(TraceBuilder instance ,ILogger logger) 
        //    => FlushAction.Invoke(logger, LogLevel.Trace, instance._settedEventId,
        //                          instance._logBuilder.ToString(), instance._settedException,
        //                          instance._objects.OrderBy(x => x.Key).Select(x => x.Value)
        //                                          .ToArray());

    }
    public abstract class MessageBuilder
    {
        internal  StringBuilder _logBuilder;
        internal  SortedList<int,object> _objects;
        internal  int _paramCount;
        internal  Exception _settedException;
        internal  EventId _settedEventId;
        internal  object _scope;

        protected MessageBuilder()
        {
            _logBuilder      = new StringBuilder();
            _objects         = new SortedList<int, object>();
            _paramCount      = 0;
            _settedException = null;
            
        }
        
        internal void AppendEvendId(EventId eventId)
        {
            
                _settedEventId = eventId;
            
        }

        internal void AppendScope(object scope)
        {
            
                _scope = scope;
           
        }

        internal void AppendException(Exception exception)
        {
            
                _settedException = exception;
          
        }
        internal void AppendMessage(string message)
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
                        message = $" {message}";
                    }
                    _logBuilder.Append(message);
                }

                
            
        }

        internal void AppendObject(object o)
        {
            AppendObjects(new[] {o});
        }

        internal void AppendObjects(params object[] objects)
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

        internal Action<ILogger, LogLevel, EventId, string, Exception, object[]> FlushAction =
            (logger, level, eventId, message, exception, objects) =>
                logger.Log(level, eventId, message, exception, objects);

    }
    
    
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