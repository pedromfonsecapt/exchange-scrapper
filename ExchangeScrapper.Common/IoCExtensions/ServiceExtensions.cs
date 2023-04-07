using ExchangeScrapper.Common.DelegatingHandlers;

namespace ExchangeScrapper.Common.IoCExtensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddCommonServices(this IServiceCollection services)
    {
        services.AddTransient<LoggingHandler>();
        return services;
    }
}