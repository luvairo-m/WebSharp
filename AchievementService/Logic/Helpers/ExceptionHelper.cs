using Dal.Entities;
using Logic.Exceptions;

namespace Logic.Helpers;

public static class ExceptionHelper
{
    public static void RaiseNotFoundWhenNull<TEntity>(TEntity? entity, Guid entityId)
    {
        if (entity != null)
            return;

        var entityType = typeof(TEntity);

        if (entityType == typeof(AchievementDal))
            throw new AchievementNotFoundException(entityId);

        if (entityType == typeof(UserDal))
            throw new UserNotFoundException(entityId);

        throw new NullReferenceException();
    }
}