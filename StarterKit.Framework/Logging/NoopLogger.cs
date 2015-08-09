using System;

namespace StarterKit.Framework.Logging
{
    public class NoopLogger: ILogger
    {
        public bool IncludeTrace { get; set; }

        public void Debug(Exception exception, string message = null, params object[] args)
        {
            // Do nothing...
        }

        public void Debug(string message = null, params object[] args)
        {
            // Do nothing...
        }

        public void Info(Exception exception, string message = null, params object[] args)
        {
            // Do nothing...
        }

        public void Info(string message = null, params object[] args)
        {
            // Do nothing...
        }

        public void Warning(Exception exception, string message = null, params object[] args)
        {
            // Do nothing...
        }

        public void Warning(string message = null, params object[] args)
        {
            // Do nothing...
        }

        public void Error(Exception exception, string message = null, params object[] args)
        {
            // Do nothing...
        }

        public void Error(string message = null, params object[] args)
        {
            // Do nothing...
        }

        public void Fatal(Exception exception, string message = null, params object[] args)
        {
            // Do nothing...
        }

        public void Fatal(string message = null, params object[] args)
        {
            // Do nothing...
        }
    }
}
