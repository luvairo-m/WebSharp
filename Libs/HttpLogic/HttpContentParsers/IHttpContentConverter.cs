using System.Net.Http.Headers;

namespace HttpLogic.HttpContentParsers;

public interface IHttpContentConverter
{
    MediaTypeHeaderValue MediaType { get; }
    HttpContent ConvertToHttpContent(object value);
    Task<TOutput?> ConvertFromHttpContent<TOutput>(HttpContent httpContent);
}