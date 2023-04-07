namespace ExchangeScrapper;

public static class HealthChecksExtension
{
    private const string BitfinexHealthPath = "/v2/platform/status";
    private const string KrakenHealthPath = "/api/v2/status.json";

    public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        var healthChecksBuilder = services.AddHealthChecks();
        healthChecksBuilder.AddUrlGroup(GetBitfinexHealthUri(configuration), name: "bitfinex");
        if (bool.TryParse(configuration["FeatureManagement:KrakenConnector"], out var isKrakenEnabled) && isKrakenEnabled)
        {
            healthChecksBuilder.AddUrlGroup(GetKrakenHealthUri(configuration), name: "kraken");
        }
        return services;
    }

    private static Uri GetBitfinexHealthUri(IConfiguration configuration)
    {
        var url = $"{configuration.GetSection("Bitfinex").Get<BitfinexConfiguration>().Url}{BitfinexHealthPath}";
        return new Uri(url);
    }

    private static Uri GetKrakenHealthUri(IConfiguration configuration)
    {
        var url = $"{configuration.GetSection("Kraken").Get<KrakenConfiguration>().Url}{KrakenHealthPath}";
        return new Uri(url);
    }
}