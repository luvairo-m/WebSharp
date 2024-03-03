namespace Dal.Repositories;

public interface IAchievementManipulations
{
    Task<bool> AddAchievementToUserAsync(Guid userGuid, Guid achievementGuid);
    Task<bool> RemoveAchievementFromUserAsync(Guid userGuid, Guid achievementGuid);
}