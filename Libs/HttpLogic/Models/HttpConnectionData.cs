namespace HttpLogic.Models;

public record struct HttpConnectionData(string ClientName)
{
    public TimeSpan? Timeout { get; set; } = null;
    public CancellationToken CancellationToken { get; set; } = default;
}