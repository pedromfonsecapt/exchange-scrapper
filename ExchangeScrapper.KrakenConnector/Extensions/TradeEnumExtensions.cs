namespace ExchangeScrapper.KrakenConnector.Extensions;

internal static class TradeEnumExtensions
{
    internal static string ToKrakenString(this TradeEnum trade) =>
        trade switch
        {
            TradeEnum.BtcEur => "BTCEUR",
            TradeEnum.BtcUsd => "BTCUSD",
            _ => throw new ArgumentOutOfRangeException(nameof(trade), trade, "Invalid trade type")
        };
}