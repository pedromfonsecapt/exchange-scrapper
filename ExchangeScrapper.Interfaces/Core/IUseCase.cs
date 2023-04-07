namespace ExchangeScrapper.Interfaces.Core;

public interface IUseCase
{
    ValueTask Handle(CancellationToken ct);
}