namespace HttpLogic.Models;

public readonly struct HttpConnectionData
{
    public string? ClientName { get; init; }
    public TimeSpan? Timeout { get; init; }
    public CancellationToken CancellationToken { get; init; }
}