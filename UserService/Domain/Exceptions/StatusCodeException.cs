namespace Domain.Exceptions;

public class StatusCodeException : Exception
{
    public int StatusCode { get; private set; }
    
    public StatusCodeException(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
}