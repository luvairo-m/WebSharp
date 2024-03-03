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

    public async Task<bool> CreateAchievementAsync(AchievementDal achievementDal)
    {
        var entity = await context.Achievements.AddAsync(achievementDal);
        await context.SaveChangesAsync();

        return entity.State == EntityState.Unchanged;
    }

    public async Task<AchievementDal?> GetAchievementAsync(Guid achievementGuid)
    {
        return await context.Achievements.FindAsync(achievementGuid);
    }

    public async Task<bool> DeleteAchievementAsync(Guid achievementGuid)
    {
        var foundEntity = await GetAchievementAsync(achievementGuid);

        if (foundEntity == null)
            return true;

        var entity = context.Achievements.Remove(foundEntity);
        await context.SaveChangesAsync();

        return entity.State == EntityState.Deleted;
    }

    public async Task<UserDal?> GetUserAsync(Guid userGuid)
    {
        return await context.Users
            .Include(user => user.Achievements)
            .FirstOrDefaultAsync(user => user.Id == userGuid);
    }

    public async Task<List<AchievementDal>> GetAllAchievements()
    {
        return await context.Achievements.ToListAsync();
    }

    public async Task<bool> AddAchievementToUserAsync(Guid userGuid, Guid achievementGuid)
    {
        var userDal = await GetUserAsync(userGuid);
        var achievementDal = await GetAchievementAsync(achievementGuid);

        if (achievementDal == null)
            return false;

        if (userDal == null)
        {
            var newUser = new UserDal
            {
                Id = userGuid,
                Achievements = new List<AchievementDal> { achievementDal }
            };

            await context.Users.AddAsync(newUser);
        }
        else
        {
            if (!userDal.Achievements.Contains(achievementDal))
                userDal.Achievements.Add(achievementDal);
        }

        await context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveAchievementFromUserAsync(Guid userGuid, Guid achievementGuid)
    {
        var userDal = await GetUserAsync(userGuid);

        if (userDal == null)
            return false;

        if (userDal.Achievements.Count == 0)
            return true;

        var achievementDal = await GetAchievementAsync(achievementGuid);

        if (achievementDal != null)
            userDal.Achievements.Remove(achievementDal);

        await context.SaveChangesAsync();

        return true;
    }
}