namespace Core.Exceptions;

public class StatusCodeException : Exception
{
    protected StatusCodeException(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }

    public int StatusCode { get; private set; }
}