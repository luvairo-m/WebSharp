using HttpLogic.Models;

namespace HttpLogic.Contracts;

public interface IHttpConnectionService
{
    HttpClient CreateHttpClient(HttpConnectionData httpConnectionData);

    Task<HttpResponseMessage> SendRequestAsync(
        HttpRequestMessage httpRequestMessage,
        HttpClient httpClient,
        CancellationToken cancellationToken,
        HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead);
}