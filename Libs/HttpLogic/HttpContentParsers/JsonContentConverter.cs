using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace HttpLogic.HttpContentParsers;

public class JsonContentConverter : IHttpContentConverter
{
    public MediaTypeHeaderValue MediaType => new("application/json");

    public HttpContent ConvertToHttpContent(object value)
    {
        var jsonString = JsonSerializer.Serialize(value);
        return new StringContent(jsonString, Encoding.UTF8, MediaType);
    }

    public async Task<TOutput?> ConvertFromHttpContent<TOutput>(HttpContent httpContent)
    {
        return await httpContent.ReadFromJsonAsync<TOutput>();
    }
}