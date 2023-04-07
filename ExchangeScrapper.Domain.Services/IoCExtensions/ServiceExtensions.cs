namespace ExchangeScrapper.Domain.Services.IocExtensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddSingleton<ITradesSummaryCalculator, TradesSummaryCalculator>();
        services.AddSingleton<ITradesSummaryViewer, SerilogTradesSummaryViewer>();
        return services;
    }
}