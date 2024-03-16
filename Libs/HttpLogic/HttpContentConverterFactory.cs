using HttpLogic.HttpContentParsers;
using HttpLogic.Models;

namespace HttpLogic;

public static class HttpContentConverterFactory
{
    public static IHttpContentConverter CreateConverter(ContentType contentType)
    {
        return contentType switch
        {
            ContentType.ApplicationJson => new JsonContentConverter(),
            ContentType.ApplicationXml => new XmlContentConverter(),
            ContentType.TextXml => new XmlContentConverter(),
            ContentType.Binary => new ByteContentConverter(),
            ContentType.XWwwFormUrlEncoded => new XWwwFormUrlEncodedConverter(),
            ContentType.TextPlain => new TextPlainContentConverter(),
            ContentType.MultipartFormData => throw new NotSupportedException(),
            ContentType.ApplicationJwt => throw new NotSupportedException(),
            ContentType.Unknown => throw new NotSupportedException(),
            _ => throw new ArgumentOutOfRangeException(nameof(contentType), contentType, null)
        };
    }
}