using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace PH.LoggingExtensions.Nlog
{
    public static class Log
    {
        
        public static Fluent Fluent(this NLog.ILogger logger,
                                   [CallerMemberName] string callerMemberName = null,
                                   [CallerFilePath] string callerFilePath = null,
                                   [CallerLineNumber] int callerLineNumber = 0)
        {
            string className = "";
            var stackTrace = new System.Diagnostics.StackTrace(1); // skip one frame as this is the Log function frame
            
            
            var method = stackTrace.GetFrame(0)?.GetMethod();
            if (null != method)
            {
                className = method?.DeclaringType?.Name;
            }

            return new Fluent(logger, className, callerMemberName, callerFilePath, callerLineNumber);
        }

        

        #region *** Trace ***

        public static NlogTraceBuilder Trace(this Fluent fluent, string message) =>
            Trace(fluent.MessageBuilder, message);
        
        public static NlogTraceBuilder Trace(this NLogMessageBuilder genericBuilder, string message)
        {
            if (genericBuilder is NlogTraceBuilder traceBuilder)
            {
                traceBuilder.AppendMessage(message);
                return traceBuilder;
            }
            var info = new NlogTraceBuilder(genericBuilder._logger, genericBuilder._evtInfo.CallerClassName,
                                            genericBuilder._evtInfo.CallerMemberName, genericBuilder._evtInfo.CallerFilePath,
                                            genericBuilder._evtInfo.CallerLineNumber);
            var msg = genericBuilder.PrepareMessage();
            if (null != msg.Message)
            {
                info.AppendMessage(msg.Message);
            }

            if (null != msg.paramters)
            {
                foreach (var msgParamter in msg.paramters)
                {
                    info.AppendObject(msgParamter);
                }
            }
            
            if (null != msg.exception)
            {
                info.SetException(msg.exception);
            }
            

            return info;
        }

        public static NlogTraceBuilder Trace(this NlogTraceBuilder genericBuilder, string message)
        {
            
            genericBuilder.AppendMessage(message);
              

            return genericBuilder;
        }

        public static NlogTraceBuilder Trace(this NlogTraceBuilder info, string message, object objectParam)
        {
            info.AppendMessage(message);
            info.AppendObject(objectParam);
            return info;
        }

        public static NlogTraceBuilder Trace(this NlogTraceBuilder info,Exception e)
        {
            info.SetException(e);
            return info;
        }

        #endregion

        #region *** Debug ***

        public static NlogDebugBuilder Debug(this Fluent fluent, string message) =>
            Debug(fluent.MessageBuilder, message);
        
        public static NlogDebugBuilder Debug(this NLogMessageBuilder genericBuilder, string message)
        {
            if (genericBuilder is NlogDebugBuilder traceBuilder)
            {
                traceBuilder.AppendMessage(message);
                return traceBuilder;
            }
            var info = new NlogDebugBuilder(genericBuilder._logger, genericBuilder._evtInfo.CallerClassName,
                                            genericBuilder._evtInfo.CallerMemberName, genericBuilder._evtInfo.CallerFilePath,
                                            genericBuilder._evtInfo.CallerLineNumber);
            var msg = genericBuilder.PrepareMessage();
            if (null != msg.Message)
            {
                info.AppendMessage(msg.Message);
            }

            if (null != msg.paramters)
            {
                foreach (var msgParamter in msg.paramters)
                {
                    info.AppendObject(msgParamter);
                }
            }
            
            if (null != msg.exception)
            {
                info.SetException(msg.exception);
            }
            

            return info;
        }

        public static NlogDebugBuilder Debug(this NlogTraceBuilder genericBuilder, string message)
        {
            if (genericBuilder is NlogDebugBuilder infoBuilder)
            {
                infoBuilder.AppendMessage(message);
                return infoBuilder;
            }
            var info = new NlogTraceBuilder(genericBuilder._logger, genericBuilder._evtInfo.CallerClassName,
                                            genericBuilder._evtInfo.CallerMemberName, genericBuilder._evtInfo.CallerFilePath,
                                            genericBuilder._evtInfo.CallerLineNumber);
            var msg = genericBuilder.PrepareMessage();
            if (null != msg.Message)
            {
                info.AppendMessage(msg.Message);
            }

            if (null != msg.paramters)
            {
                foreach (var msgParamter in msg.paramters)
                {
                    info.AppendObject(msgParamter);
                }
            }
            
            if (null != msg.exception)
            {
                info.SetException(msg.exception);
            }
            

            return info;
        }

        public static NlogTraceBuilder Trace(this NlogTraceBuilder info, string message, object objectParam)
        {
            info.AppendMessage(message);
            info.AppendObject(objectParam);
            return info;
        }

        public static NlogTraceBuilder Trace(this NlogTraceBuilder info,Exception e)
        {
            info.SetException(e);
            return info;
        }

        #endregion

        #region *** Info ***

        public static NlogInfoBuilder Info(this Fluent fluent, string message) => Info(fluent.MessageBuilder, message);
        
        public static NlogInfoBuilder Info(this NLogMessageBuilder genericBuilder, string message)
        {
            if (genericBuilder is NlogInfoBuilder infoBuilder)
            {
                infoBuilder.AppendMessage(message);
                return infoBuilder;
            }
            var info = new NlogInfoBuilder(genericBuilder._logger, genericBuilder._evtInfo.CallerClassName,
                                           genericBuilder._evtInfo.CallerMemberName, genericBuilder._evtInfo.CallerFilePath,
                                           genericBuilder._evtInfo.CallerLineNumber);
            var msg = genericBuilder.PrepareMessage();
            if (null != msg.Message)
            {
                info.AppendMessage(msg.Message);
            }

            if (null != msg.paramters)
            {
                foreach (var msgParamter in msg.paramters)
                {
                    info.AppendObject(msgParamter);
                }
            }
            
            if (null != msg.exception)
            {
                info.SetException(msg.exception);
            }
            

            return info;
        }
        
        public static NlogInfoBuilder Info(this NlogInfoBuilder info, string message)
        {
            info.AppendMessage(message);
            return info;
        }

        

        public static NlogInfoBuilder Info(this NlogInfoBuilder info, string message, object objectParam)
        {
            info.AppendMessage(message);
            info.AppendObject(objectParam);
            return info;
        }

        
        public static NlogInfoBuilder Info(this NlogInfoBuilder info,Exception e)
        {
            info.SetException(e);
            return info;
        }

        #endregion
        
       
    }
}
