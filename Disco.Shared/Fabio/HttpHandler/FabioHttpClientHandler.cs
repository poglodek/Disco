using Disco.Shared.Fabio.Options;

namespace Disco.Shared.Fabio.HttpHandler;

public class FabioHttpClientHandler : DelegatingHandler
{
    private readonly FabioOptions _options;

    public FabioHttpClientHandler(FabioOptions options)
    {
        _options = options;
    }

    protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.RequestUri = GetFabioUri(request);
        
        return base.Send(request, cancellationToken);
    }

    private Uri GetFabioUri(HttpRequestMessage request)
        => new($"{_options.Url}/{request.RequestUri.Host}/{request.RequestUri.PathAndQuery}");
}