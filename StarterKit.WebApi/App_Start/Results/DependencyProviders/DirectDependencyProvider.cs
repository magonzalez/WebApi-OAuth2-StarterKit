using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace StarterKit.WebApi.Results.DependencyProviders
{
    public class DirectDependencyProvider : IDependencyProvider
    {
        private readonly bool _includeErrorDetail;
        private readonly IContentNegotiator _contentNegotiator;
        private readonly HttpRequestMessage _request;
        private readonly IEnumerable<MediaTypeFormatter> _formatters;

        public DirectDependencyProvider(bool includeErrorDetail, IContentNegotiator contentNegotiator, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
        {
            if (contentNegotiator == null)
            {
                throw new ArgumentNullException("contentNegotiator");
            }

            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            if (formatters == null)
            {
                throw new ArgumentNullException("formatters");
            }

            _includeErrorDetail = includeErrorDetail;
            _contentNegotiator = contentNegotiator;
            _request = request;
            _formatters = formatters;
        }

        public bool IncludeErrorDetail
        {
            get { return _includeErrorDetail; }
        }

        public IContentNegotiator ContentNegotiator
        {
            get { return _contentNegotiator; }
        }

        public HttpRequestMessage Request
        {
            get { return _request; }
        }

        public IEnumerable<MediaTypeFormatter> Formatters
        {
            get { return _formatters; }
        }
    }
}
