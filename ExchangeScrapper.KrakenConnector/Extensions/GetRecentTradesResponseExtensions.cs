namespace ExchangeScrapper.KrakenConnector.Extensions;

internal static class GetRecentTradesResponseExtensions
{
    internal static IEnumerable<IEnumerable<string>> GetTrades(this GetRecentTradesResponse trades, TradeEnum trade) =>
        trade switch
        {
            TradeEnum.BtcEur => trades.Result.BtcEur,
            TradeEnum.BtcUsd => trades.Result.BtcUsd,
            _ => throw new ArgumentOutOfRangeException(nameof(trade), trade, "Invalid trade type")
        };
}