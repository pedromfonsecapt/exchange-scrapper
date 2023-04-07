namespace ExchangeScrapper.Core.UnitTests;

public class CalculateTradesSummaryJobTests
{
    private Mock<ICalculateTradesSummaryUseCase> _useCase;
    private Mock<ILogger<TradesSummaryJob>> _logger;
    private JobsConfiguration _config;
    private IPeriodicTimerJob _job;

    [SetUp]
    public void Setup()
    {
        _useCase = new Mock<ICalculateTradesSummaryUseCase>();
        _logger = new Mock<ILogger<TradesSummaryJob>>();
        _config = new JobsConfiguration
        {
            TradesSummaryJobPeriodSeconds = 10
        };
        _job = new TradesSummaryJob(_useCase.Object, _logger.Object, _config);
    }

    [Test]
    public void SetupTest() => Assert.Pass();

    [TestCase(5000, 0)]
    [TestCase(15000, 1)]
    [TestCase(25000, 2)]
    public void UseCaseIsInvoked(int seconds, int expectedCalls)
    {
        var cts = new CancellationTokenSource();
        _job.Start(cts.Token);
        Thread.Sleep(seconds);
        cts.Cancel();
        _useCase.Verify(x => x.Handle(cts.Token), Times.Exactly(expectedCalls));
    }
}