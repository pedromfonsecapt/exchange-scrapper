namespace ExchangeScrapper.Interfaces.TradesSummaryUseCase;

public interface ITradesSummaryCalculator
{
    TradeSummary Calculate(IEnumerable<Trade> trades);
}