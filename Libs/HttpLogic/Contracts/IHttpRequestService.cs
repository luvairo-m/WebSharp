using HttpLogic.Models;
using Polly;

namespace HttpLogic.Contracts;

public interface IHttpRequestService
{
    Task<HttpResponseData<TResponse>> SendRequestAsync<TResponse>(
        HttpRequestData requestData,
        HttpConnectionData connectionData = default,
        IAsyncPolicy? policy = null);
}