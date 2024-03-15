using HttpLogic.Models;
using Polly;

namespace HttpLogic.Contracts;

public interface IHttpConnectionService
{
    HttpClient CreateHttpClient(HttpConnectionData httpConnectionData);

    Task<HttpResponseMessage> SendRequestAsync(
        HttpClient httpClient,
        HttpRequestMessage httpRequestMessage,
        IAsyncPolicy? policy = null,
        HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead,
        CancellationToken cancellationToken = default);
}