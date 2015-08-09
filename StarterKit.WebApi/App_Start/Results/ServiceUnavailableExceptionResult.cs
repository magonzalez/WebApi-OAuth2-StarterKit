using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using StarterKit.Framework.Exceptions;
using StarterKit.WebApi.Models;
using StarterKit.WebApi.Results.DependencyProviders;

namespace StarterKit.WebApi.Results
{
    /// <summary>
    /// Represents an action result that returns a <see cref="HttpStatusCode.BadRequest"/> response and performs
    /// content negotiation on an <see cref="HttpError"/> based on a <see cref="ServiceUnavailableException"/>.
    /// </summary>
    public class ServiceUnavailableExceptionResult : IHttpActionResult
    {
        private readonly ServiceUnavailableException _exception;
        private readonly IDependencyProvider _dependencies;

        /// <summary>Initializes a new instance of the <see cref="InvalidModelStateResult"/> class.</summary>
        /// <param name="exception">The ServiceUnavailable exception information to include in the error.</param>
        /// <param name="includeErrorDetail">
        /// <see langword="true"/> if the error should include exception messages; otherwise, <see langword="false"/>.
        /// </param>
        /// <param name="contentNegotiator">The content negotiator to handle content negotiation.</param>
        /// <param name="request">The request message which led to this result.</param>
        /// <param name="formatters">The formatters to use to negotiate and format the content.</param>
        public ServiceUnavailableExceptionResult(
            ServiceUnavailableException exception,
            bool includeErrorDetail,
            IContentNegotiator contentNegotiator,
            HttpRequestMessage request,
            IEnumerable<MediaTypeFormatter> formatters)
            : this(exception, new DirectDependencyProvider(includeErrorDetail, contentNegotiator, request, formatters))
        {
        }

        /// <summary>Initializes a new instance of the <see cref="InvalidModelStateResult"/> class.</summary>
        /// <param name="exception">The ServiceUnavailable exception information to include in the error.</param>
        /// <param name="controller">The controller from which to obtain the dependencies needed for execution.</param>
        public ServiceUnavailableExceptionResult(ServiceUnavailableException exception, ApiController controller)
            : this(exception, new ApiControllerDependencyProvider(controller))
        {
        }

        private ServiceUnavailableExceptionResult(
            ServiceUnavailableException exception, IDependencyProvider dependencies)
        {
            if (exception == null)
            {
                throw new ArgumentNullException("exception");
            }

            Contract.Assert(dependencies != null);

            _exception = exception;
            _dependencies = dependencies;
        }

        /// <summary>Gets the ServiceUnavailable exception to include in the error.</summary>
        public ServiceUnavailableException Exception
        {
            get { return _exception; }
        }

        /// <summary>Gets a value indicating whether the error should include exception messages.</summary>
        public bool IncludeErrorDetail
        {
            get { return _dependencies.IncludeErrorDetail; }
        }

        /// <summary>Gets the content negotiator to handle content negotiation.</summary>
        public IContentNegotiator ContentNegotiator
        {
            get { return _dependencies.ContentNegotiator; }
        }

        /// <summary>Gets the request message which led to this result.</summary>
        public HttpRequestMessage Request
        {
            get { return _dependencies.Request; }
        }

        /// <summary>Gets the formatters to use to negotiate and format the content.</summary>
        public IEnumerable<MediaTypeFormatter> Formatters
        {
            get { return _dependencies.Formatters; }
        }

        /// <inheritdoc />
        public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }

        private HttpResponseMessage Execute()
        {
            var httpResponseMessage = new HttpResponseMessage();

            try
            {
                var negotiationResult = ContentNegotiator.Negotiate(typeof(HttpError), Request, Formatters);

                if (negotiationResult == null)
                {
                    httpResponseMessage.StatusCode = HttpStatusCode.NotAcceptable;
                }
                else
                {
                    var error = new HttpError("Service Unavailable");
                    foreach (var err in _exception.Errors)
                    {
                        if (!error.ContainsKey(err.ItemName))
                        {
                            error.Add(err.ItemName, new Collection<ApiError>());
                        }

                        ((ICollection<ApiError>)error[err.ItemName]).Add(
                            new ApiError { ErrorCode = err.ErrorCode, Message = err.ErrorMessage });
                    }

                    httpResponseMessage.StatusCode = HttpStatusCode.ServiceUnavailable;
                    httpResponseMessage.Content = new ObjectContent<HttpError>(
                        error, negotiationResult.Formatter, negotiationResult.MediaType);
                }

                httpResponseMessage.RequestMessage = Request;
            }
            catch
            {
                httpResponseMessage.Dispose();
                throw;
            }

            return httpResponseMessage;
        }
    }
}
