using HttpLogic.Contracts;
using HttpLogic.Models;

namespace HttpLogic.Services;

public class HttpRequestService : IHttpRequestService
{
    public Task<HttpResponse<TResponse>> SendRequestAsync<TResponse>(
        HttpRequestData requestData,
        HttpConnectionData connectionData = default)
    {
        throw new NotImplementedException();
    }
}