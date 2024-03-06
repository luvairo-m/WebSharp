using System.Net;
using Core.Exceptions;

namespace Logic.Exceptions;

public class UserNotFoundException : StatusCodeException
{
    public UserNotFoundException(Guid userId)
        : base($"User [{userId}] not found!", (int)HttpStatusCode.NotFound)
    {
    }
}