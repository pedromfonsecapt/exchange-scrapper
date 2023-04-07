namespace ExchangeScrapper.Interfaces.TradesSummaryUseCase;

public interface ITradesCollector
{
    Task<IEnumerable<Trade>> CollectLastTrades(TradeEnum trade, DateTimeOffset since, CancellationToken ct);
}