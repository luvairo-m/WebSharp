namespace HttpLogic.Models;

public record HttpRequestData
{
    public HttpMethod Method { get; init; } = null!;
    public Uri Uri { get; init; } = null!;
    public object Body { get; init; } = null!;
    public ContentType ContentType { get; init; } = ContentType.ApplicationJson;
    public IDictionary<string, string> HeaderDictionary { get; init; } = new Dictionary<string, string>();
    public IDictionary<string, string> QueryDictionary { get; init; } = new Dictionary<string, string>();
}