using ExchangeScrapper.Common;
using Microsoft.FeatureManagement;

namespace ExchangeScrapper.KrakenConnector.Services;

public class KrakenConnector : ITradesCollector
{
    private const string VersionPath = "/0";
    private const string PublicPath = "/public";
    private const string TradesPath = "/Trades";

    private readonly HttpClient _client;
    private readonly KrakenConfiguration _configuration;
    private readonly IFeatureManager _featureManager;

    public KrakenConnector(HttpClient client, KrakenConfiguration configuration, IFeatureManager featureManager)
    {
        _client = client;
        _configuration = configuration;
        _featureManager = featureManager;
    }

    // Check https://docs.kraken.com/rest/#tag/Market-Data/operation/getRecentTrades
    public async Task<IEnumerable<Trade>> CollectLastTrades(TradeEnum trade, DateTimeOffset since, CancellationToken ct)
    {
        if (!await _featureManager.IsEnabledAsync(FeatureFlags.FeatureKrakenConnector))
        {
            return Enumerable.Empty<Trade>();
        }
        var sinceEpoch = since.GetMillisecondEpochTimestamp();
        var path = $"{_configuration.Url}{VersionPath}{PublicPath}{TradesPath}?pair={trade.ToKrakenString()}&since={sinceEpoch}";
        var tradesResponse = await _client.ExecuteHttpRequest<GetRecentTradesResponse>(path, ct);
        var trades = tradesResponse.GetTrades(trade);
        return trades.Select(x =>
        {
            var price = x.ElementAt(0);
            var priceParsed = double.Parse(price);
            return new Trade
            {
                Price = priceParsed
            };
        });
    }
}