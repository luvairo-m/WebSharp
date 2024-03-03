using Dal.Entities;

namespace Dal.Repositories;

public interface IAchievementRepository : IAchievementManipulations
{
    Task<bool> CreateAchievementAsync(AchievementDal achievementDal);
    Task<AchievementDal?> GetAchievementAsync(Guid achievementGuid);
    Task<bool> DeleteAchievementAsync(Guid achievementGuid);
    Task<UserDal?> GetUserAsync(Guid userGuid);
    Task<List<AchievementDal>> GetAllAchievements();
}