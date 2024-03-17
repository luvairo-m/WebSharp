using HttpLogic.Contracts;

namespace HttpLogic.Services;

internal class HttpConnectionService : IHttpConnectionService
{
    private readonly IHttpClientFactory clientFactory;

    public HttpConnectionService(IHttpClientFactory clientFactory)
    {
        this.clientFactory = clientFactory;
    }

    public HttpClient CreateHttpClient(string? clientName = null, TimeSpan? timeOut = null)
    {
        var client = string.IsNullOrWhiteSpace(clientName)
            ? clientFactory.CreateClient()
            : clientFactory.CreateClient(clientName);

        if (timeOut != null)
            client.Timeout = timeOut.Value;

        return client;
    }

    public async Task<HttpResponseMessage> SendRequestAsync(
        HttpClient httpClient,
        HttpRequestMessage httpRequestMessage,
        HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead,
        CancellationToken cancellationToken = default)
    {
        return await httpClient.SendAsync(httpRequestMessage, httpCompletionOption, cancellationToken);
    }
}