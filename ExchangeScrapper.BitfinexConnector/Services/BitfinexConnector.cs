namespace ExchangeScrapper.BitfinexConnector.Services;

public class BitfinexConnector : ITradesCollector
{
    private const int Limit = 10000;
    private const string VersionPath = "/v2";
    private const string TradesPath = "/trades";
    private const string HistoryPath = "/hist";

    private readonly HttpClient _client;
    private readonly BitfinexConfiguration _configuration;

    public BitfinexConnector(HttpClient client, BitfinexConfiguration configuration)
    {
        _client = client;
        _configuration = configuration;
    }

    // Check https://docs.bitfinex.com/reference/rest-public-trades
    public async Task<IEnumerable<Trade>> CollectLastTrades(TradeEnum trade, DateTimeOffset since, CancellationToken ct)
    {
        var sinceEpoch = since.GetMillisecondEpochTimestamp();
        var path = $"{_configuration.Url}{VersionPath}{TradesPath}/{trade.ToBitfinexString()}{HistoryPath}?start={sinceEpoch}&limit={Limit}";
        var trades = await _client.ExecuteHttpRequest<IEnumerable<IEnumerable<double>>>(path, ct);
        return trades.Select(x => new Trade
        {
            Price = x.ElementAt(3)
        });
    }
}