using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace StarterKit.WebApi.Results.DependencyProviders
{
    public interface IDependencyProvider
    {
        bool IncludeErrorDetail { get; }

        IContentNegotiator ContentNegotiator { get; }

        HttpRequestMessage Request { get; }

        IEnumerable<MediaTypeFormatter> Formatters { get; }
    }
}
