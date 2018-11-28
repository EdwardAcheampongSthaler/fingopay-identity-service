using System;
using NLog;
using STH.BiometricIdentityService.Infrastructure.Interfaces;

namespace STH.BiometricIdentityService.Infrastructure
{
    public class AppLogger: IAppLogger
    {
        private readonly NLog.Logger _logger;

        private AppLogger(string name)
        {
            _logger = LogManager.GetLogger(name);
        }

        private AppLogger(Type type)
        {
            _logger = LogManager.GetLogger(type.Name);
        }

        public static AppLogger Create(Type type)
        {
            return new AppLogger(type);
        }

        public static AppLogger Create(string name)
        {
            return new AppLogger(name);
        }

        private void WriteMessage(LogLevel level, string message, Exception exception = null)
        {
            // create log event from the passed message            
            var logEvent = new LogEventInfo(level, _logger.Name, message);
            if (exception != null)
            {
                logEvent.Exception = exception;
                // use code below if want to send exception to app fail service
                //    try
                //    {
                //        Appfail.SendToAppfail(exception);
                //    }
                //    catch (Exception)
                //    {
                //        //Do nothing   
                //    }
            }

            // Call the Log() method. It is important to pass typeof(MyLogger) as the            
            // first parameter. If you don't, ${callsite} and other callstack-related             
            // layout renderers will not work properly
            _logger.Log(typeof (AppLogger), logEvent);
        }

        public void WarnException(string message, Exception ex, params object[] args)
        {
            WriteMessage(LogLevel.Warn, FormatMessage(message, args), ex);
        }

        public void Warn(string message, params object[] args)
        {
            WriteMessage(LogLevel.Warn, FormatMessage(message, args));
        }

        public void Info(string message, params object[] args)
        {
            WriteMessage(LogLevel.Info, FormatMessage(message, args));
        }

        public void DebugException(string message, Exception ex, params object[] args)
        {
            WriteMessage(LogLevel.Debug, FormatMessage(message, args), ex);
        }

        public void Debug(string message, params object[] args)
        {
            WriteMessage(LogLevel.Debug, FormatMessage(message, args));
        }

        public void ErrorException(string message, Exception ex, params object[] args)
        {
            WriteMessage(LogLevel.Error, FormatMessage(message, args), ex);
        }

        private static string FormatMessage(string message, object[] args)
        {
            if (args == null || args.Length == 0)
                return message;
            return string.Format(message, args);
        }

        public void Error(string message, params object[] args)
        {
            WriteMessage(LogLevel.Error, FormatMessage(message, args));
        }

        public void FatalException(string message, Exception ex, params object[] args)
        {
            WriteMessage(LogLevel.Fatal, FormatMessage(message, args), ex);
        }

        public void Fatal(string message, params object[] args)
        {
            WriteMessage(LogLevel.Fatal, FormatMessage(message, args));
        }
    }
}