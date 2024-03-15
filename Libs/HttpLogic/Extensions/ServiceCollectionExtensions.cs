using HttpLogic.Contracts;
using HttpLogic.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HttpLogic.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHttpLogic(this IServiceCollection collection)
    {
        collection.AddHttpClient();

        collection.TryAddTransient<IHttpConnectionService, HttpConnectionService>();
        collection.TryAddTransient<IHttpRequestService, HttpRequestService>();

        return collection;
    }
}