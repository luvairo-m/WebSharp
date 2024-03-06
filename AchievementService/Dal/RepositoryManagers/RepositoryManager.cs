using Dal.Repositories.AchievementRepository;
using Dal.Repositories.UserRepository;

namespace Dal.RepositoryManagers;

public class RepositoryManager : IRepositoryManager
{
    private readonly AchievementContext achievementContext;

    public RepositoryManager(
        AchievementContext achievementContext,
        IAchievementRepository achievementRepository,
        IUserRepository userRepository)
    {
        this.achievementContext = achievementContext;
        Achievements = achievementRepository;
        Users = userRepository;
    }

    public IAchievementRepository Achievements { get; }

    public IUserRepository Users { get; }

    public async Task<int> SaveChangesAsync(CancellationToken token = default)
    {
        return await achievementContext.SaveChangesAsync(token);
    }
}