namespace ExchangeScrapper.Common.RetryPolicies;

public static class HttpRetryPolicies
{
    public static IAsyncPolicy<HttpResponseMessage> GetHttpRetryPolicy<TConnector>(IServiceProvider serviceProvider, HttpRequestMessage requestMessage)
    {
        var logger = serviceProvider.GetService<ILogger<TConnector>>();
        var policy = HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(3,
                i => TimeSpan.FromSeconds(1),
                async (result, timeSpan, retryCount, context) =>
                {
                    await OnHttpRetry(result, timeSpan, retryCount, logger);
                });
        return policy;
    }

    private static Task OnHttpRetry(DelegateResult<HttpResponseMessage> result, TimeSpan timeSpan, int retryCount, ILogger logger)
    {
       logger.LogError($"Request failed with '{HttpStatusCode.Unauthorized}'. Waiting '{timeSpan}' before next retry. Retry attempt '{retryCount}'", result.Exception);
       return Task.CompletedTask;
    }
}