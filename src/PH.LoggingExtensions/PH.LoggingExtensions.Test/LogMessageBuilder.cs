using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Extensions.Logging;

namespace PH.LoggingExtensions.Test
{
    public class LogMessageBuilder
    {
        private readonly StringBuilder _builder;
        private readonly Dictionary<int,object> _objectsParameters;
        private int objCount;
        private string _callMemberName;
        private string _callerFilePath;
        private int _callerLineNumber;
        private readonly Microsoft.Extensions.Logging.LogLevel _level;
        public LogMessageBuilder(Microsoft.Extensions.Logging.LogLevel level = LogLevel.Trace)
        {
            _builder           = new StringBuilder();
            _objectsParameters = new Dictionary<int, object>();
            objCount           = 0;
            _level             = level;
        }

        private void AppendMessages(string message)
        {
            if (!string.IsNullOrEmpty(message) && !string.IsNullOrWhiteSpace(message))
            {
                string m = message.Trim();
                if (_builder.Length > 0)
                {
                    m = " " + m;
                }    
                _builder.Append(m);
                
            }
        }

        private void AppendObjectParam(object obj)
        {
            _objectsParameters.Add(objCount,obj);
            objCount++;
        }

        public LogMessageBuilder Message(string message)
        {
            AppendMessages(message);
            return this;
        }
        public LogMessageBuilder Message(string message,object param)
        {
            AppendMessages(message);
            AppendObjectParam(param);
            return this;
        }

        public LogMessageBuilder Message(string message,params object[] objects)
        {
            AppendMessages(message);
            foreach (var o in objects)
            {
                AppendObjectParam(o);
            }
            return this;
        }

        public void Flush([CallerMemberName] string callerMemberName = null,
                          [CallerFilePath] string callerFilePath = null,
                          [CallerLineNumber] int callerLineNumber = 0)
        {
            _callMemberName   = callerMemberName;
            _callerFilePath   = callerFilePath;
            _callerLineNumber = callerLineNumber;
        }

        

        public void Reset()
        {
            _builder.Clear();
            _objectsParameters.Clear();
            objCount = 0;
        }

        internal (string MessageLog, object[] ParamObjects, Microsoft.Extensions.Logging.LogLevel LogLevel) GetMessage()
        {
            var m = _builder.ToString();
            var o = _objectsParameters.OrderBy(x => x.Key).Select(x => x.Value).ToArray();
            return (m, o, _level);
        }

    }
}