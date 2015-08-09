using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

using StarterKit.Framework.Exceptions;
using StarterKit.Framework.Logging;
using StarterKit.WebApi.Results;

namespace StarterKit.WebApi.Handlers
{
    public class ApiExceptionHandler : ExceptionHandler
    {
        private readonly ILogger _logger;

        public ApiExceptionHandler(ILogger logger)
        {
            _logger = logger;
        }

        public override void Handle(ExceptionHandlerContext context)
        {
            if (context != null)
            {
                // An exception handler indicates that it has handled an exception by setting the
                // Result property to an action result (for example, an ExceptionResult,
                // InternalServerErrorResult, StatusCodeResult, or a custom result). 
                // If the Result property is null, the exception is unhandled and the original
                // exception will be re-thrown.
                context.Result = HandleExceptionResult(context.ExceptionContext);
            }
        }

        private IHttpActionResult HandleExceptionResult(ExceptionContext context)
        {
            Exception exception = context.Exception;

            HttpRequestMessage request = context.Request;
            if (request == null)
            {
                return null;
            }

            HttpRequestContext requestContext = context.RequestContext;
            if (requestContext == null)
            {
                return null;
            }

            HttpConfiguration configuration = requestContext.Configuration;
            if (configuration == null)
            {
                return null;
            }

            IContentNegotiator contentNegotiator = configuration.Services.GetContentNegotiator();
            if (contentNegotiator == null)
            {
                return null;
            }

            return GetExceptionActionResult(exception, requestContext.IncludeErrorDetail, contentNegotiator, request,
                configuration.Formatters);
        }

        private IHttpActionResult GetExceptionActionResult(
            Exception exception,
            bool includeErrorDetail,
            IContentNegotiator contentNegotiator,
            HttpRequestMessage request,
            IEnumerable<MediaTypeFormatter> formatters)
        {
            // Add a ReferenceId property to the exception so we can log it and/or return it to the caller.
            exception.AddReferenceId();

            if (exception is NotAuthorizedException)
                return new StatusCodeResult(HttpStatusCode.Unauthorized, request);

            if (exception is ForbiddenException)
                return new StatusCodeResult(HttpStatusCode.Forbidden, request);

            var validationException = exception as ValidationException;
            if (validationException != null)
                return new ValidationExceptionResult(validationException, includeErrorDetail, contentNegotiator, request, formatters);

            if (exception is DuplicateException)
                return new ConflictResult(request);

            if (exception is NotFoundException)
                return new NotFoundResult(request);

            _logger.Error(exception, "Received exception while processing request.\n{0}", request.RequestUri);

            var unavailableException = exception as ServiceUnavailableException;
            if (unavailableException != null)
                return new ServiceUnavailableExceptionResult(
                    unavailableException, includeErrorDetail, contentNegotiator, request, formatters);
            
            return new ReferencedExceptionResult(exception, includeErrorDetail, contentNegotiator, request, formatters);
        }
    }
}