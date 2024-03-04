using Dal.Entities;
using Dal.Repositories.BaseRepository;

namespace Dal.Repositories.AchievementRepository;

public interface IAchievementRepository : IRepositoryBase<AchievementDal>
{
    Task<IEnumerable<AchievementDal>> GetAllAchievementsAsync(CancellationToken token = default);

    Task<AchievementDal?> GetAchievementByIdAsync(
        Guid achievementId,
        bool trackEntity = true,
        CancellationToken token = default);

    void CreateAchievement(AchievementDal achievement);
    void UpdateAchievement(AchievementDal achievement);
    void DeleteAchievement(AchievementDal achievement);
}