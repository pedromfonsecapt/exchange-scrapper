namespace ExchangeScrapper.Domain.Services.UnitTests;

public class TradeSummaryCalculatorTests
{
    private ITradesSummaryCalculator _tradesSummaryCalculator;

    [SetUp]
    public void Setup()
    {
        _tradesSummaryCalculator = new TradesSummaryCalculator();
    }

    [Test]
    public void SetupTest() => Pass();

    [TestCase(1,2,3, 2.0d, 1, 3)]
    [TestCase(1, 1, 1, 1d, 1, 1)]
    public void VerifyTradesSummary(int price1, int price2, int price3, double expectedAveragePrice, int expectedMinPrice, int expectedMaxPrice)
    {
        var trades = new List<Trade>
        {
            new()
            {
                Price = price1
            },
            new()
            {
                Price = price2
            },
            new()
            {
                Price = price3
            }
        };
        var summary = _tradesSummaryCalculator.Calculate(trades);
        Multiple(() =>
        {
            That(expectedAveragePrice, Is.EqualTo(summary.AveragePrice));
            That(expectedMinPrice, Is.EqualTo(summary.MinPrice));
            That(expectedMaxPrice, Is.EqualTo(summary.MaxPrice));
        });
    }
}