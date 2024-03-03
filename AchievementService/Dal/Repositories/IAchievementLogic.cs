using Dal.Entities;

namespace Dal.Repositories;

public interface IAchievementLogic
{
    Task<ICollection<AchievementDal>> GetUserAchievementsAsync(Guid userGuid);
    Task<bool> AddAchievementToUserAsync(Guid userGuid, Guid achievementGuid);
    Task<bool> RemoveAchievementFromUserAsync(Guid userGuid, Guid achievementGuid);
}