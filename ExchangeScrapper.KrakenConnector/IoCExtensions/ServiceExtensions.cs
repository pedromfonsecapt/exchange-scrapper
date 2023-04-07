namespace ExchangeScrapper.KrakenConnector.IoCExtensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddKrakenConnector(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(configuration.GetSection("Kraken").Get<KrakenConfiguration>());
        services.AddHttpClient<ITradesCollector, Services.KrakenConnector>()
            .AddHttpMessageHandler<LoggingHandler>()
            .AddPolicyHandler(HttpRetryPolicies.GetHttpRetryPolicy<Services.KrakenConnector>);
        return services;
    }
}