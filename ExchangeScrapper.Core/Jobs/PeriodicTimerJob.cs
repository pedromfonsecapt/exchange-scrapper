namespace ExchangeScrapper.Core.Jobs;

public abstract class PeriodicTimerJob : IPeriodicTimerJob
{
    private readonly IUseCase _useCase;
    private readonly ILogger _logger;
    private readonly int _periodSeconds;

    protected  PeriodicTimerJob(IUseCase useCase, ILogger logger, int periodSeconds)
    {
        _useCase = useCase;
        _logger = logger;
        _periodSeconds = periodSeconds;
    }

    public async ValueTask Start(CancellationToken ct)
    {
        using PeriodicTimer timer = new(TimeSpan.FromSeconds(_periodSeconds));
        while (await timer.WaitForNextTickAsync(ct))
        {
            try
            {
                await _useCase.Handle(ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unable to execute use case '{_useCase.GetType().Name}' successfully");
            }
        }
    }
}