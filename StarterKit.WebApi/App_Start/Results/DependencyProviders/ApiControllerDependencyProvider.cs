using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace StarterKit.WebApi.Results.DependencyProviders
{
    public class ApiControllerDependencyProvider : IDependencyProvider
    {
        private readonly ApiController _controller;

        private IDependencyProvider _resolvedDependencies;

        public ApiControllerDependencyProvider(ApiController controller)
        {
            if (controller == null)
            {
                throw new ArgumentNullException("controller");
            }

            _controller = controller;
        }

        public bool IncludeErrorDetail
        {
            get
            {
                EnsureResolved();
                return _resolvedDependencies.IncludeErrorDetail;
            }
        }

        public IContentNegotiator ContentNegotiator
        {
            get
            {
                EnsureResolved();
                return _resolvedDependencies.ContentNegotiator;
            }
        }

        public HttpRequestMessage Request
        {
            get
            {
                EnsureResolved();
                return _resolvedDependencies.Request;
            }
        }

        public IEnumerable<MediaTypeFormatter> Formatters
        {
            get
            {
                EnsureResolved();
                return _resolvedDependencies.Formatters;
            }
        }

        private void EnsureResolved()
        {
            if (_resolvedDependencies == null)
            {
                HttpRequestContext requestContext = _controller.RequestContext;
                Contract.Assert(requestContext != null);
                bool includeErrorDetail = requestContext.IncludeErrorDetail;

                HttpConfiguration configuration = _controller.Configuration;

                if (configuration == null)
                {
                    throw new InvalidOperationException("Configuration must not be null.");
                }

                ServicesContainer services = configuration.Services;
                Contract.Assert(services != null);
                IContentNegotiator contentNegotiator = services.GetContentNegotiator();

                if (contentNegotiator == null)
                {
                    throw new InvalidOperationException("ContentNegotiator must not be null.");
                }

                HttpRequestMessage request = _controller.Request;

                if (request == null)
                {
                    throw new InvalidOperationException("Request must not be null.");
                }

                IEnumerable<MediaTypeFormatter> formatters = configuration.Formatters;
                Contract.Assert(formatters != null);

                _resolvedDependencies = new DirectDependencyProvider(includeErrorDetail, contentNegotiator, request, formatters);
            }
        }
    }
}
