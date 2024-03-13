using HttpLogic.Models;

namespace HttpLogic.Contracts;

public interface IHttpRequestService
{
    Task<HttpResponse<TResponse>> SendRequestAsync<TResponse>(
        HttpRequestData requestData,
        HttpConnectionData connectionData = default);
}