using System;
using System.Diagnostics;
using System.Web.Http.ExceptionHandling;
using StarterKit.Framework.Logging;

namespace StarterKit.WebApi.Handlers
{
    /// <summary>
    /// Implementation of <see cref="T:System.Web.Http.ExceptionHandling.ExceptionLogger" /> that logs to <see cref="T:LightSail.Core.ILightSailLogger" />.
    /// </summary>
    public class ApiExceptionLogger : ExceptionLogger
    {
        private readonly ILogger _logger;

        public ApiExceptionLogger(ILogger logger)
        {
            _logger = logger;
        }

        public override void Log(ExceptionLoggerContext context)
        {
            try
            {
                // The CallsHandler property allows a logger to identify exceptions that cannot be
                // handled. When the connection is about to be aborted and no new response message
                // can be sent, the loggers will be called but the handler will not be called, and
                // the loggers can identify this scenario from this property. We will only log
                // something in here when the handler is NOT going to be called. For any other
                // exception, let the handler deal with it.
                if (!context.CallsHandler)
                {
                    _logger.Error(context.Exception);
                }
            }
            catch (Exception ex)
            {
                // Hopefully, this block would never be reached.
                // We will just trace here.
                Trace.TraceError(ex.GetBaseException().ToString());
            }
        }
    }
}
