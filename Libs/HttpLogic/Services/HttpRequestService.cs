using HttpLogic.Contracts;
using HttpLogic.Models;
using Microsoft.AspNetCore.WebUtilities;
using Polly;
using ContentType = HttpLogic.Models.ContentType;

namespace HttpLogic.Services;

internal class HttpRequestService : IHttpRequestService
{
    private readonly IHttpConnectionService connectionService;

    public HttpRequestService(IHttpConnectionService connectionService)
    {
        this.connectionService = connectionService;
    }

    public async Task<HttpResponseData<TResponse>> SendRequestAsync<TResponse>(
        HttpRequestData requestData,
        HttpConnectionData connectionData = default,
        IAsyncPolicy? policy = null)
    {
        var client = connectionService.CreateHttpClient(connectionData);
        var httpRequestMessage = CreateHttpRequestMessage(requestData);

        var responseMessage = await connectionService
            .SendRequestAsync(client, httpRequestMessage, policy, cancellationToken: connectionData.CancellationToken);

        var bodyContent = await GetBodyOfType<TResponse>(responseMessage);

        return new HttpResponseData<TResponse>
        {
            Body = bodyContent,
            Headers = responseMessage.Headers,
            StatusCode = responseMessage.StatusCode,
            ContentHeaders = responseMessage.Content.Headers
        };
    }

    private static async Task<TResponse?> GetBodyOfType<TResponse>(HttpResponseMessage responseMessage)
    {
        var contentType = ExtractContentType(responseMessage);
        var httpConverter = HttpContentConverterFactory.CreateConverter(contentType);

        return await httpConverter.ConvertFromHttpContent<TResponse>(responseMessage.Content);
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

    private static ContentType ExtractContentType(HttpResponseMessage response)
    {
        if (response.Content.Headers.ContentType == null)
            return ContentType.Unknown;

        var mediaType = response.Content.Headers.ContentType.MediaType;

        return mediaType switch
        {
            "application/json" => ContentType.ApplicationJson,
            "application/x-www-form-urlencoded" => ContentType.XWwwFormUrlEncoded,
            "application/xml" => ContentType.ApplicationXml,
            "multipart/form-data" => ContentType.MultipartFormData,
            "text/xml" => ContentType.TextXml,
            "text/plain" => ContentType.TextPlain,
            "application/jwt" => ContentType.ApplicationJwt,
            _ => ContentType.Unknown
        };
    }
}