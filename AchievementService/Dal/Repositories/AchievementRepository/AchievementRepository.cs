using Dal.Entities;
using Dal.Repositories.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace Dal.Repositories.AchievementRepository;

public class AchievementRepository : RepositoryBase<AchievementDal>, IAchievementRepository
{
    public AchievementRepository(AchievementContext achievementContext) : base(achievementContext)
    {
    }

    public async Task<IEnumerable<AchievementDal>> GetAllAchievementsAsync(CancellationToken token = default)
    {
        return await achievementContext.Achievements
            .AsNoTracking()
            .ToListAsync(token);
    }

    public async Task<AchievementDal?> GetAchievementByIdAsync(
        Guid achievementId,
        bool trackEntity = true,
        CancellationToken token = default)
    {
        var keyValues = new object[] { achievementId };

        return trackEntity
            ? await achievementContext.Achievements.FindAsync(keyValues, token)
            : await achievementContext.Achievements
                .AsNoTracking()
                .FirstOrDefaultAsync(achievement => achievement.Id == achievementId, token);
    }

    public void CreateAchievement(AchievementDal achievement)
    {
        Create(achievement);
    }

    public void UpdateAchievement(AchievementDal achievement)
    {
        Update(achievement);
    }

    public void DeleteAchievement(AchievementDal achievement)
    {
        Delete(achievement);
    }
}