using System;
using System.Diagnostics;
using System.Web;
using log4net;
using log4net.Config;
using log4net.Core;

using StarterKit.Framework.Exceptions;

namespace StarterKit.Framework.Logging.Log4Net
{
    public class Log4NetLogger : ILogger
    {
        private readonly ILog _log;

        public static void Configure()
        {
            XmlConfigurator.Configure();
        }

        public bool IncludeTrace { get; set; } //if true do a stack trace and include the source line number the logger was called from on every line

        public Log4NetLogger()
        {
            _log = LogManager.GetLogger("Logger");
        }

        protected string Prefix
        { //add this data to every line
            get
            {
                string prefix;
                if (IncludeTrace)
                {
                    var st = new StackTrace();
                    StackFrame f = null;
                    var n = st.FrameCount;
                    for (var i = 1; i < n; i++)
                    {
                        f = st.GetFrame(i);
                        if (f.GetMethod().DeclaringType != typeof(Log4NetLogger))
                            break;
                    }
                    if (f == null)
                        prefix = "[no stack frame]";
                    else
                        prefix = f.GetFileName() + ":" + f.GetFileLineNumber() + " " + f.GetMethod().Name + "()";
                }
                else
                    prefix = "";

                var hc = HttpContext.Current;
                if (hc != null)
                {
                    var machineId = hc.Application["MachineId"] as string;
                    if (machineId != null)
                        prefix += " machine:" + machineId;

                    var reqId = hc.Items.Contains("__requestId__") ? hc.Items["__requestId__"] as string : null;
                    if (reqId != null)
                        prefix += " req:" + reqId;

                    var t0 = hc.Items.Contains("__requestStartTime__") ? (DateTime)hc.Items["__requestStartTime__"] : default(DateTime);
                    if (t0 != default(DateTime))
                    {
                        var dt = Math.Floor((DateTime.UtcNow - t0).TotalMilliseconds) / 1000.0;
                        prefix += " t:" + dt + "s";
                    }
                }

                prefix += " ";
                return prefix;
            }
        }

        protected void SetupCustomProperties(Exception e)
        {
            var properties = e.GetCustomProperties();

            foreach (var property in properties)
            {
                LogicalThreadContext.Properties[property.Key] = property.Value;
            }
        }

        //one very generic log method to implement all others
        public void Log(Level level, Exception e = null, string fmt = null, params object[] args)
        {
            if ((e != null))
                SetupCustomProperties(e);

            _log.Logger.Log(typeof(Log4NetLogger), level, Prefix + (args.Length > 0 ? string.Format(fmt ?? "", args) : fmt), e);
        }

        //per level calls as expected by the current client code
        public void Debug(Exception exception, string message = null, params object[] args)
        {
            Log(Level.Debug, exception, message, args);
        }
        public void Debug(string message = null, params object[] args)
        {
            Log(Level.Debug, null, message, args);
        }

        public void Info(Exception exception, string message = null, params object[] args)
        {
            Log(Level.Info, exception, message, args);
        }
        public void Info(string message = null, params object[] args)
        {
            Log(Level.Info, null, message, args);
        }

        public void Warning(Exception exception, string message = null, params object[] args)
        {
            Log(Level.Warn, exception, message, args);
        }
        public void Warning(string message = null, params object[] args)
        {
            Log(Level.Warn, null, message, args);
        }

        public void Error(Exception exception, string message = null, params object[] args)
        {
            Log(Level.Error, exception, message, args);
        }
        public void Error(string message = null, params object[] args)
        {
            Log(Level.Error, null, message, args);
        }

        public void Fatal(Exception exception, string message = null, params object[] args)
        {
            Log(Level.Fatal, exception, message, args);
        }
        public void Fatal(string message = null, params object[] args)
        {
            Log(Level.Fatal, null, message, args);
        }
    }
}
