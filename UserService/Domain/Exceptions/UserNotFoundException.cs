using System.Net;

namespace Domain.Exceptions;

public class UserNotFoundException : StatusCodeException
{
    public UserNotFoundException(Guid userId) 
        : base($"User [{userId}] not found!", (int)HttpStatusCode.NotFound)
    {
    }
}