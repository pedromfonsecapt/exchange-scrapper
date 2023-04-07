namespace ExchangeScrapper.Core.UseCases;

public class TradesSummaryUseCase : ICalculateTradesSummaryUseCase
{
    private readonly IEnumerable<ITradesCollector> _collectors;
    private readonly ITradesSummaryCalculator _tradesSummaryCalculator;
    private readonly ITradesSummaryViewer _tradesSummaryViewer;
    private readonly TradesSummaryUseCaseConfiguration _config;

    public TradesSummaryUseCase(IEnumerable<ITradesCollector> collectors,
                                ITradesSummaryCalculator tradesSummaryCalculator,
                                ITradesSummaryViewer tradesSummaryViewer,
                                TradesSummaryUseCaseConfiguration config)
    {
        _collectors = collectors;
        _tradesSummaryCalculator = tradesSummaryCalculator;
        _tradesSummaryViewer = tradesSummaryViewer;
        _config = config;
    }

    public async ValueTask Handle(CancellationToken ct)
    {
        var since = DateTimeOffset.UtcNow.AddSeconds(-_config.SinceSeconds);
        var trades = new List<Trade>();
        foreach (var collector in _collectors)
        {
            trades.AddRange(await collector.CollectLastTrades(_config.Trade, since, ct));
        }
        var summary = _tradesSummaryCalculator.Calculate(trades);
        _tradesSummaryViewer.View(summary);
    }
}