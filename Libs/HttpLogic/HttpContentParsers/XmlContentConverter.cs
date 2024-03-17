using System.Net.Http.Headers;
using System.Text;
using System.Xml.Serialization;

namespace HttpLogic.HttpContentParsers;

public class XmlContentConverter : IHttpContentConverter
{
    public MediaTypeHeaderValue MediaType => new("application/xml");

    public HttpContent ConvertToHttpContent(object value)
    {
        var stringBuilder = new StringBuilder();
        var xmlSerializer = new XmlSerializer(value.GetType());

        using var writer = new StringWriter(stringBuilder);
        xmlSerializer.Serialize(writer, value);

        return new StringContent(stringBuilder.ToString(), Encoding.UTF8, MediaType);
    }

    public async Task<TOutput?> ConvertFromHttpContent<TOutput>(HttpContent httpContent)
    {
        var stringContent = await httpContent.ReadAsStringAsync();
        var xmlSerializer = new XmlSerializer(typeof(TOutput));

        using var reader = new StringReader(stringContent);
        return (TOutput)xmlSerializer.Deserialize(reader)!;
    }
}