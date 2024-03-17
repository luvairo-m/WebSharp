using AchievementServiceConnectionLib.Models;

namespace AchievementServiceConnectionLib.Contracts;

public interface IAchievementConnectionService
{
    Task<IEnumerable<AchievementDto>> GetUserAchievementsAsync(
        Guid userId,
        CancellationToken cancellationToken = default);
}