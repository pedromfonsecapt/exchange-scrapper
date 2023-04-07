namespace ExchangeScrapper.BitfinexConnector.Extensions;

internal static class TradeEnumExtensions
{
    internal static string ToBitfinexString(this TradeEnum trade) =>
        trade switch
        {
            TradeEnum.BtcEur => "tBTCEUR",
            TradeEnum.BtcUsd => "tBTCUSD",
            _ => throw new ArgumentOutOfRangeException(nameof(trade), trade, "Invalid trade type")
        };
}