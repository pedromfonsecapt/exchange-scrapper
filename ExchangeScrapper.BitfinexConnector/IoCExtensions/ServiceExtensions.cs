namespace ExchangeScrapper.BitfinexConnector.IoCExtensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddBitfinexConnector(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(configuration.GetSection("Bitfinex").Get<BitfinexConfiguration>());
        services.AddHttpClient<ITradesCollector, Services.BitfinexConnector>()
            .AddHttpMessageHandler<LoggingHandler>()
            .AddPolicyHandler(HttpRetryPolicies.GetHttpRetryPolicy<Services.BitfinexConnector>);
        return services;
    }
}