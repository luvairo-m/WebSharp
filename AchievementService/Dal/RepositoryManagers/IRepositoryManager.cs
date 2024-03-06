using Dal.Repositories.AchievementRepository;
using Dal.Repositories.UserRepository;

namespace Dal.RepositoryManagers;

public interface IRepositoryManager
{
    IAchievementRepository Achievements { get; }
    IUserRepository Users { get; }
    Task<int> SaveChangesAsync(CancellationToken token = default);
}