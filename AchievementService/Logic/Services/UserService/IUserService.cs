using Logic.Entities;

namespace Logic.Services.UserService;

public interface IUserService
{
    Task<IEnumerable<AchievementDto>> GetUserAchievementsAsync(Guid userId, CancellationToken token = default);
    Task AddAchievementToUserAsync(Guid userId, Guid achievementId, CancellationToken token = default);
    Task RemoveAchievementFromUserAsync(Guid userId, Guid achievementId, CancellationToken token = default);
}