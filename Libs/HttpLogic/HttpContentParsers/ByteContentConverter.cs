namespace HttpLogic.HttpContentParsers;

public class ByteContentConverter : IHttpContentConverter
{
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