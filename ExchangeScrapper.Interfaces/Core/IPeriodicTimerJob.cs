namespace ExchangeScrapper.Interfaces.Core;

public interface IPeriodicTimerJob
{
    ValueTask Start(CancellationToken ct);
}