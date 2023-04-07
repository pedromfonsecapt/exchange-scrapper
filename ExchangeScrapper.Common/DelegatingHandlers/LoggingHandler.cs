namespace ExchangeScrapper.Common.DelegatingHandlers;

public class LoggingHandler : DelegatingHandler
{
    private readonly ILogger<LoggingHandler> _logger;

    public LoggingHandler(ILogger<LoggingHandler> logger)
    {
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        string requestContent = null;
        if (request.Content != null)
        {
            requestContent = await request.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            _logger.LogDebug(requestContent);
        }
        _logger.LogDebug($"Request '{request.Method}' '{request.RequestUri}' Content '{requestContent}'");
        var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        _logger.LogDebug($"Response '{request.Method} '{request.RequestUri} Status Code '{response.StatusCode}' Content '{responseContent}'");
        return response;
    }
}