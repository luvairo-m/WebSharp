using HttpLogic.Contracts;
using HttpLogic.Models;
using Polly;

namespace HttpLogic.Services;

internal class HttpConnectionService : IHttpConnectionService
{
    private readonly IHttpClientFactory clientFactory;

    public HttpConnectionService(IHttpClientFactory clientFactory)
    {
        this.clientFactory = clientFactory;
    }

    public HttpClient CreateHttpClient(HttpConnectionData httpConnectionData)
    {
        var client = string.IsNullOrWhiteSpace(httpConnectionData.ClientName)
            ? clientFactory.CreateClient()
            : clientFactory.CreateClient(httpConnectionData.ClientName);

        if (httpConnectionData.Timeout != null)
            client.Timeout = httpConnectionData.Timeout.Value;

        return client;
    }

    public async Task<HttpResponseMessage> SendRequestAsync(
        HttpClient httpClient,
        HttpRequestMessage httpRequestMessage,
        IAsyncPolicy? policy = null,
        HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead,
        CancellationToken cancellationToken = default)
    {
        policy ??= Policy.NoOpAsync();
        return await policy.ExecuteAsync(async () =>
            await httpClient.SendAsync(httpRequestMessage, httpCompletionOption, cancellationToken));
    }
}