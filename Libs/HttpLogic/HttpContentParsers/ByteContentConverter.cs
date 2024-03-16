using MediaTypeHeaderValue = System.Net.Http.Headers.MediaTypeHeaderValue;

namespace HttpLogic.HttpContentParsers;

public class ByteContentConverter : IHttpContentConverter
{
    /// <summary>
    ///     We don't actually know what is encoded in byte array.
    ///     That's why we pass string.Empty as ContentType header value.
    /// </summary>
    public MediaTypeHeaderValue MediaType => new(string.Empty);

    public HttpContent ConvertToHttpContent(object value)
    {
        if (value.GetType() == typeof(byte[]))
            return new ByteArrayContent((byte[])value);

        throw new Exception($"Bad value for {nameof(ByteArrayContent)}");
    }

    public async Task<TOutput?> ConvertFromHttpContent<TOutput>(HttpContent httpContent)
    {
        var byteArray = await httpContent.ReadAsByteArrayAsync();
        return (TOutput)(object)byteArray;
    }
}