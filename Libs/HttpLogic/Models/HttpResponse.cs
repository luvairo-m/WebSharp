namespace HttpLogic.Models;

public abstract record HttpResponse<TResponse> : BaseHttpResponse
{
    public TResponse? Body { get; set; }
}