using System;

namespace StarterKit.Framework.Logging
{
    public interface ILogger
    {
        bool IncludeTrace { get; set; }
        void Debug(Exception exception, string message = null, params object[] args);
        void Debug(string message = null, params object[] args);
        void Info(Exception exception, string message = null, params object[] args);
        void Info(string message = null, params object[] args);
        void Warning(Exception exception, string message = null, params object[] args);
        void Warning(string message = null, params object[] args);
        void Error(Exception exception, string message = null, params object[] args);
        void Error(string message = null, params object[] args);
        void Fatal(Exception exception, string message = null, params object[] args);
        void Fatal(string message = null, params object[] args);
    }
}