using System.Net;
using Core.Exceptions;

namespace Logic.Exceptions;

public class AchievementNotFoundException : StatusCodeException
{
    public AchievementNotFoundException(Guid achievementId)
        : base($"Achievement [{achievementId}] not found!", (int)HttpStatusCode.NotFound)
    {
    }
}