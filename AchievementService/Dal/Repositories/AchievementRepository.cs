using Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dal.Repositories;

public class AchievementRepository : IAchievementRepository
{
    private readonly AchievementContext context;

    public AchievementRepository(AchievementContext achievementContext)
    {
        context = achievementContext;
    }

    public async Task<Guid> CreateAchievementAsync(AchievementDal achievement)
    {
        var generated = achievement with { Id = Guid.NewGuid() };

        await context.Achievements.AddAsync(generated);
        await context.SaveChangesAsync();

        return generated.Id;
    }

    public async Task<AchievementDal?> GetAchievementAsync(Guid achievementGuid)
    {
        return await context.Achievements.FindAsync(achievementGuid);
    }

    public async Task<Guid> DeleteAchievementAsync(Guid achievementGuid)
    {
        var foundEntity = await GetAchievementAsync(achievementGuid);

        if (foundEntity == null)
            return achievementGuid;

        context.Achievements.Remove(foundEntity);
        await context.SaveChangesAsync();

        return achievementGuid;
    }

    public async Task<UserDal?> GetUserAsync(Guid userGuid)
    {
        return await context.Users
            .Include(user => user.Achievements)
            .FirstOrDefaultAsync(user => user.Id == userGuid);
    }

    public async Task<ICollection<AchievementDal>> GetUserAchievementsAsync(Guid userGuid)
    {
        var userDal = await GetUserAsync(userGuid);
        return userDal == null ? new List<AchievementDal>() : userDal.Achievements;
    }

    public async Task<bool> AddAchievementToUserAsync(Guid userGuid, Guid achievementGuid)
    {
        var userDal = await GetUserAsync(userGuid);
        var achievementDal = await GetAchievementAsync(achievementGuid);

        if (userDal == null || achievementDal == null)
            return false;

        userDal.Achievements.Add(achievementDal);

        return true;
    }

    public async Task<bool> RemoveAchievementFromUserAsync(Guid userGuid, Guid achievementGuid)
    {
        var userDal = await GetUserAsync(userGuid);

        if (userDal == null || userDal.Achievements.Count == 0)
            return true;

        var achievementDal = await GetAchievementAsync(achievementGuid);

        if (achievementDal != null)
            userDal.Achievements.Remove(achievementDal);

        return true;
    }
}