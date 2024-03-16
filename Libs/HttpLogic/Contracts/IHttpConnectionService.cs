namespace HttpLogic.Contracts;

public interface IHttpConnectionService
{
    HttpClient CreateHttpClient(string? clientName = null, TimeSpan? timeOut = null);

    Task<HttpResponseMessage> SendRequestAsync(
        HttpClient httpClient,
        HttpRequestMessage httpRequestMessage,
        HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead,
        CancellationToken cancellationToken = default);
}