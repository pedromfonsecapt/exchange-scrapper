namespace ExchangeScrapper.Core.Jobs;

public class TradesSummaryJob : PeriodicTimerJob
{
    public TradesSummaryJob(ICalculateTradesSummaryUseCase useCase, ILogger<TradesSummaryJob> logger, JobsConfiguration config) 
        : base(useCase, logger, config.TradesSummaryJobPeriodSeconds)
    {
    }
}