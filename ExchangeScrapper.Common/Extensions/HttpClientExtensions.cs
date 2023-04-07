namespace ExchangeScrapper.Common.Extensions;

public static class HttpClientExtensions
{
    private const string ApplicationJson = "application/json";

    public static async Task<TResult> ExecuteHttpRequest<TResult>(this HttpClient client, string url, CancellationToken ct)
    {
        using var httpRequestMessage = new HttpRequestMessage
        {
            RequestUri = new Uri(url, UriKind.Absolute),
            Method = HttpMethod.Get,
            Headers = { { HttpRequestHeader.Accept.ToString(), ApplicationJson } },
        };
        using var httpResponseMessage = await client.SendAsync(httpRequestMessage, ct);
        httpResponseMessage.EnsureSuccessStatusCode();
        var responseStr = await httpResponseMessage.Content.ReadAsStringAsync(ct);
        return JsonConvert.DeserializeObject<TResult>(responseStr);
    }
}