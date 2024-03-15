using HttpLogic.Models;

namespace HttpLogic.Contracts;

public interface IHttpConnectionService
{
    HttpClient CreateHttpClient(HttpConnectionData httpConnectionData);

    Task<HttpResponseMessage> SendRequestAsync(
        HttpRequestMessage httpRequestMessage,
        HttpClient httpClient,
        HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead,
        CancellationToken cancellationToken = default);
}