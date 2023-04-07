namespace ExchangeScrapper.Core.Configuration;

public class UseCasesConfiguration
{
    public TradesSummaryUseCaseConfiguration TradesSummary { get; set; }
}

public class TradesSummaryUseCaseConfiguration
{
    public TradeEnum Trade { get; set; }
    public int SinceSeconds { get; set; } = 3600;
}