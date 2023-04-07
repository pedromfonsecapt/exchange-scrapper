namespace ExchangeScrapper.Common.Extensions;

public static class DateTimeExtensions
{
    public static double GetMillisecondEpochTimestamp(this DateTimeOffset dateTimeOffset)
    {
        return dateTimeOffset.ToUnixTimeMilliseconds();
    }
}