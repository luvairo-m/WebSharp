using System.Net;
using System.Net.Http.Headers;

namespace HttpLogic.Models;

public record BaseHttpResponse
{
    public HttpStatusCode StatusCode { get; set; }
    public HttpResponseHeaders Headers { get; set; } = null!;
    public HttpContentHeaders ContentHeaders { get; init; } = null!;
    public bool IsSuccessStatusCode => (int)StatusCode is >= 200 and <= 299;
}