using Logic.Entities;

namespace Logic.Services.AchievementService;

public interface IAchievementService
{
    Task<IEnumerable<AchievementDto>> GetAllAchievementsAsync(CancellationToken token = default);
    Task<AchievementDto?> GetAchievementByIdAsync(Guid achievementId, CancellationToken token = default);
    Task<Guid> CreateAchievementAsync(AchievementDto achievementDto, CancellationToken token = default);
    Task DeleteAchievementAsync(Guid achievementId, CancellationToken token = default);
    Task UpdateAchievementAsync(AchievementDto achievementDto, CancellationToken token = default);
}