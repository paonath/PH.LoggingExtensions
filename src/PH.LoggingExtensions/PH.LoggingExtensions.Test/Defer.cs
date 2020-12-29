using System;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;

namespace PH.LoggingExtensions.Test
{
    public static class Defer2Ext
    {
        //public static InfoEvent Info2(this ILogger logger, string message = "")
        //{
        //    InfoEvent eEvent = new InfoEvent(message);
        //    eEvent.InnerFlush = (sender, evt) =>
        //    {
        //        logger.LogInformation(evt._message);
        //    };
        //    return eEvent;
        //}

        //public static void Flush(this InfoEvent infoEvent)
        //{
        //    infoEvent.OnFlush(infoEvent);
        //}


        public static InfoB InfoB(this ILogger logger)
        {
            
            var i = new InfoB(logger);
            //i.Log += delegate(string message) { logger.LogInformation(message);  };

            return i;
        }

        public static void Flush(this InfoB b)
        {
            b.OnFlushLog();
        }

    }

    public class InfoB
    {
        private string _message;

        // Define a delegate named LogHandler, which will encapsulate
        // any method that takes a string as the parameter and returns no value
        public delegate void LogHandler(string message);
 
        // Define an Event based on the above Delegate
        public event LogHandler Log;

        internal InfoB(ILogger logger)
        {
            //Log += delegate(string message) { logger.LogInformation(message); };
            Log += message => logger.LogInformation(message);
        }

        
        
        public InfoB Info(string message)
        {
            _message += message;
            return this;
        }

        internal void OnFlushLog()
        {
            if (Log != null && !string.IsNullOrEmpty(_message) && !string.IsNullOrWhiteSpace(_message))
            {
                Log(_message);
            }
        }

        
    }

    public class InfoEvent : EventArgs
    {
        internal string _message;

        public InfoEvent(string message = "")
        {
            _message = message;
        }

        public InfoEvent Info(string message)
        {
            if (!string.IsNullOrEmpty(_message))
            {
                _message += " " + message;
            }
            else
            {
                _message = message;
            }

            return this;
        }

        public EventHandler<InfoEvent> InnerFlush;

        internal  void OnFlush(InfoEvent e)
        {
            EventHandler<InfoEvent> handler = InnerFlush;
            handler.Invoke(this,e);
        }
    }
    
    
    public class Defer
    {
        internal readonly Action _flushLog;
        private string _message;
        internal readonly ILogger _logger;
        public Defer(ILogger logger, string message = "")
        {
            _logger   = logger;
            _flushLog = new Action(() => _logger.LogInformation(_message));
            if (!string.IsNullOrEmpty(message))
            {
                _message = message;
            }
        }

        private void Action()
        {
            _logger.LogInformation(_message);
        }

        public Defer Info(string message)
        {
            _message += message;
            return this;
        }
        
        
    }
}