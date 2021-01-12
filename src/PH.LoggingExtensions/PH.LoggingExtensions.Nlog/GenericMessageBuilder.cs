using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PH.LoggingExtensions.Nlog
{
    public abstract class GenericMessageBuilder
    {
        internal readonly StringBuilder _builder;
        internal readonly Dictionary<int, object> _obectParameters;
        internal int _objectCount;
        protected  bool Empty;
        internal Exception _exception;

        protected GenericMessageBuilder()
        {
            _builder         = new StringBuilder();
            _obectParameters = new Dictionary<int, object>();
            _objectCount     = 0;
            Empty            = true;
        }

        protected internal void SetException(Exception exception)
        {
            _exception = exception;
        }
        protected internal void AppendMessage(string message, bool includeStartingWitheSpace = true)
        {
            if (!string.IsNullOrEmpty(message) && !string.IsNullOrWhiteSpace(message))
            {
                string m = message.Trim();
                if (_builder.Length > 0 && includeStartingWitheSpace)
                {
                    _builder.Append(" ");
                }    
                _builder.Append(m);
                Empty = false;
            }
        }

        protected internal void AppendObject(object o)
        {
            _obectParameters.Add(_objectCount,o);
            _objectCount++;
            Empty = false;
        }

        public void Reset()
        {
            _builder.Clear();
            _obectParameters.Clear();
            _objectCount = 0;
            _exception   = null;
            Empty        = true;
        }

        internal (string Message, object[] paramters, Exception exception) PrepareMessage()
        {
            if (Empty)
            {
                return (null, null,null);
            }

            return (_builder.ToString(), _obectParameters.OrderBy(x => x.Key).Select(x => x.Value).ToArray(),_exception);
        }
    }
}