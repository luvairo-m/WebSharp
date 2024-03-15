using System.Net.Http.Json;

namespace HttpLogic.HttpContentParsers;

public class JsonContentConverter : IHttpContentConverter
{
    public HttpContent ConvertToHttpContent(object value)
    {
        return JsonContent.Create(value);
    }

    public async Task<TOutput?> ConvertFromHttpContent<TOutput>(HttpContent httpContent)
    {
        return await httpContent.ReadFromJsonAsync<TOutput>();
    }
}