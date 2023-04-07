using Newtonsoft.Json;

namespace ExchangeScrapper.KrakenConnector.Contracts;

internal class GetRecentTradesResponse
{
    public IEnumerable<string> Error { get; set; }
    public GetRecentTradesResult Result { get; set; }
}

internal class GetRecentTradesResult
{
    public string Last { get; set; }

    [JsonProperty("XXBTZEUR")]
    public IEnumerable<IEnumerable<string>> BtcEur { get; set; }

    [JsonProperty("XXBTZUSD")]
    public IEnumerable<IEnumerable<string>> BtcUsd { get; set; }
}