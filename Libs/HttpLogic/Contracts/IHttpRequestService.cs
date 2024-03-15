using HttpLogic.Models;

namespace HttpLogic.Contracts;

public interface IHttpRequestService
{
    Task<HttpResponseData<TResponse>> SendRequestAsync<TResponse>(
        HttpRequestData requestData,
        HttpConnectionData connectionData = default);
}