using Dal.Entities;

namespace Dal.Repositories;

public interface IAchievementRepository : IAchievementLogic
{
    Task<Guid> CreateAchievementAsync(AchievementDal achievement);
    Task<AchievementDal?> GetAchievementAsync(Guid achievementGuid);
    Task<Guid> DeleteAchievementAsync(Guid achievementGuid);
    Task<UserDal?> GetUserAsync(Guid userGuid);
}