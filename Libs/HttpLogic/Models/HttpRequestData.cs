namespace HttpLogic.Models;

public abstract record HttpRequestData
{
    public HttpMethod Method { get; set; } = null!;
    public Uri Uri { set; get; } = null!;
    public object Body { get; set; } = null!;
    public ContentType ContentType { get; set; } = ContentType.ApplicationJson;
    public IDictionary<string, string> HeaderDictionary { get; set; } = new Dictionary<string, string>();

    public ICollection<KeyValuePair<string, string>> QueryParameterList { get; set; } =
        new List<KeyValuePair<string, string>>();
}