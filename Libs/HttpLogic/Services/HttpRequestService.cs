using HttpLogic.Contracts;
using HttpLogic.Extensions;
using HttpLogic.Models;
using Microsoft.AspNetCore.WebUtilities;
using Polly;
using ContentType = HttpLogic.Models.ContentType;

namespace HttpLogic.Services;

internal class HttpRequestService : IHttpRequestService
{
    private static readonly IDictionary<string, ContentType> contentTypes =
        new Dictionary<string, ContentType>
        {
            ["application/json"] = ContentType.ApplicationJson,
            ["application/x-www-form-urlencoded"] = ContentType.XWwwFormUrlEncoded,
            ["application/xml"] = ContentType.ApplicationXml,
            ["text/xml"] = ContentType.TextXml,
            ["text/plain"] = ContentType.TextPlain,
            ["application/jwt"] = ContentType.ApplicationJwt,
            ["multipart/form-data"] = ContentType.MultipartFormData
        };

    private readonly IHttpConnectionService connectionService;

    public HttpRequestService(IHttpConnectionService connectionService)
    {
        this.connectionService = connectionService;
    }

    public async Task<HttpResponseData<TResponse>> SendRequestAsync<TResponse>(
        HttpRequestData requestData,
        HttpConnectionData connectionData = default,
        IAsyncPolicy<HttpResponseMessage>? resiliencePolicy = null,
        CancellationToken cancellationToken = default) where TResponse : class
    {
        var client = connectionService.CreateHttpClient(connectionData.ClientName, connectionData.TimeOut);
        var httpRequestMessage = CreateHttpRequestMessage(requestData);

        resiliencePolicy ??= Policy.NoOpAsync<HttpResponseMessage>();

        var responseMessage = await resiliencePolicy.ExecuteAsync(async () =>
            await connectionService
                .SendRequestAsync(
                    client,
                    await httpRequestMessage.ShallowCloneAsync(),
                    cancellationToken: cancellationToken));

        var bodyContent = await GetBodyOfTypeAsync<TResponse>(responseMessage);

        return new HttpResponseData<TResponse>
        {
            Body = bodyContent,
            Headers = responseMessage.Headers,
            StatusCode = responseMessage.StatusCode,
            ContentHeaders = responseMessage.Content.Headers
        };
    }

    private static async Task<TResponse?> GetBodyOfTypeAsync<TResponse>(HttpResponseMessage responseMessage)
        where TResponse : class
    {
        var contentType = responseMessage.Content.Headers.ContentType;

        if (contentType == null || !responseMessage.IsSuccessStatusCode)
            return null;

        if (!contentTypes.TryGetValue(contentType.MediaType!, out var foundType))
            throw new NotSupportedException($"{contentType.MediaType!} ContentType not supported!");

        return await HttpContentConverterFactory
            .CreateConverter(foundType)
            .ConvertFromHttpContent<TResponse>(responseMessage.Content);
    }

    private static HttpRequestMessage CreateHttpRequestMessage(HttpRequestData requestData)
    {
        var httpConverter = HttpContentConverterFactory.CreateConverter(requestData.ContentType);
        var requestUri = new Uri(
            QueryHelpers.AddQueryString(requestData.Uri.AbsoluteUri, requestData.QueryDictionary));

        var httpRequestMessage = new HttpRequestMessage
        {
            Method = requestData.Method,
            RequestUri = requestUri,
            Content = httpConverter.ConvertToHttpContent(requestData.Body)
        };

        foreach (var headerPair in requestData.HeaderDictionary)
            httpRequestMessage.Headers.Add(headerPair.Key, headerPair.Value);

        return httpRequestMessage;
    }
}