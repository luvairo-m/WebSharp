using Dal.Entities;
using Logic.Entities;

namespace Logic.Managers;

public interface IAchievementManager
{
    Task<AchievementDto?> GetAchievementAsync(Guid achievementGuid);
    Task<Guid> CreateAchievementAsync(AchievementDto achievementDto);
    Task<Guid> DeleteAchievementAsync(Guid achievementGuid);
    Task<List<AchievementDto>?> GetUserAchievementsAsync(Guid userGuid);
    Task<bool> AddAchievementToUserAsync(Guid userGuid, Guid achievementGuid);
    Task<bool> RemoveAchievementFromUserAsync(Guid userGuid, Guid achievementGuid);
    Task<List<AchievementDto>> GetAllAchievements();
}