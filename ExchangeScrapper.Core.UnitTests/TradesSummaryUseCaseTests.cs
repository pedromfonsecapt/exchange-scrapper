namespace ExchangeScrapper.Core.UnitTests;

public class TradesSummaryUseCaseTests
{
    private ICalculateTradesSummaryUseCase _useCase;
    private Mock<ITradesCollector> _collector;
    private Mock<ITradesSummaryCalculator> _tradesSummaryCalculatorMock;
    private Mock<ITradesSummaryViewer> _tradesSummaryViewer;
    private TradesSummaryUseCaseConfiguration _config;

    [SetUp]
    public void Setup()
    {
        _collector = new Mock<ITradesCollector>();
        _tradesSummaryCalculatorMock = new Mock<ITradesSummaryCalculator>();
        _tradesSummaryViewer = new Mock<ITradesSummaryViewer>();
        _config = new TradesSummaryUseCaseConfiguration { Trade = TradeEnum.BtcEur };
        _useCase = new TradesSummaryUseCase(new List<ITradesCollector> { _collector.Object }, _tradesSummaryCalculatorMock.Object, _tradesSummaryViewer.Object, _config);
    }

    [Test]
    public void SetupTest() => Assert.Pass();

    [Test]
    public async Task TradesDataIsFetchedOnce()
    {
        await _useCase.Handle(CancellationToken.None);
        _collector.Verify(x => x.CollectLastTrades(_config.Trade, 
            It.IsInRange(DateTimeOffset.UtcNow.AddMinutes(-61), DateTimeOffset.UtcNow.AddMinutes(-59), Range.Inclusive),
            CancellationToken.None), Times.Exactly(1));
    }

    [Test]
    public async Task TradesSummaryIsCalculatedOnce()
    {
        await _useCase.Handle(CancellationToken.None);
        _tradesSummaryCalculatorMock.Verify(x => x.Calculate(It.IsAny<IEnumerable<Trade>>()), Times.Exactly(1));
    }

    [Test]
    public async Task TradesSummaryIsDisplayedOnce()
    {
        await _useCase.Handle(CancellationToken.None);
        _tradesSummaryViewer.Verify(x => x.View(It.IsAny<TradeSummary>()), Times.Exactly(1));
    }
}