using Logic.Entities;

namespace Logic.Managers;

public interface IAchievementManager
{
    Task<Guid> CreateAchievementAsync(AchievementDto achievementDto);
    Task<Guid> DeleteAchievementAsync(Guid achievementGuid);
    Task<AchievementDto?> GetAchievementAsync(Guid achievementGuid);
}