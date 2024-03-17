namespace HttpLogic.Models;

public record HttpResponseData<TResponse> : BaseHttpResponse
{
    public TResponse? Body { get; init; }
}