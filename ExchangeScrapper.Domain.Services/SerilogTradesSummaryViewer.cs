namespace ExchangeScrapper.Domain.Services;

public class SerilogTradesSummaryViewer : ITradesSummaryViewer
{
    private readonly ILogger<SerilogTradesSummaryViewer> _logger;

    public SerilogTradesSummaryViewer(ILogger<SerilogTradesSummaryViewer> logger)
    {
        _logger = logger;
    }

    public void View(TradeSummary summary)
    {
        _logger.LogInformation("Trade Summary for last hour: {@summary}", summary);
    }
}