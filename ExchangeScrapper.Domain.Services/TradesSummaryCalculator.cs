namespace ExchangeScrapper.Domain.Services;

public class TradesSummaryCalculator : ITradesSummaryCalculator
{
    public TradeSummary Calculate(IEnumerable<Trade> trades)
    {
        return new TradeSummary
        {
            AveragePrice = trades.Average(x => x.Price),
            MaxPrice = trades.Max(x => x.Price),
            MinPrice = trades.Min(x => x.Price)
        };
    }
}