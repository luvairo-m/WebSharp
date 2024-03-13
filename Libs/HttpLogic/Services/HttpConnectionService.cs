using HttpLogic.Contracts;
using HttpLogic.Models;

namespace HttpLogic.Services;

public class HttpConnectionService : IHttpConnectionService
{
    public HttpClient CreateHttpClient(HttpConnectionData httpConnectionData)
    {
        throw new NotImplementedException();
    }

    public Task<HttpResponseMessage> SendRequestAsync(
        HttpRequestMessage httpRequestMessage,
        HttpClient httpClient,
        CancellationToken cancellationToken,
        HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead)
    {
        throw new NotImplementedException();
    }
}