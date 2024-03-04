using AutoMapper;
using Dal.Entities;
using Dal.RepositoryManagers;
using Logic.Entities;

namespace Logic.Services.UserService;

public class UserService : IUserService
{
    private readonly IMapper mapper;
    private readonly IRepositoryManager repositoryManager;

    public UserService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        this.repositoryManager = repositoryManager;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<AchievementDto>> GetUserAchievementsAsync(
        Guid userId,
        CancellationToken token = default)
    {
        var userDal = await repositoryManager.Users.GetUserByIdAsync(userId, token);

        if (userDal == null)
            throw new Exception("User not found!");

        return userDal.Achievements.Select(mapper.Map<AchievementDto>);
    }

    // TODO: Добавить генерацию кастомного исключения при отсутствии Пользователя или Достижения
    public async Task AddAchievementToUserAsync(Guid userId, Guid achievementId, CancellationToken token = default)
    {
        var achievementDal = await repositoryManager.Achievements
            .GetAchievementByIdAsync(achievementId, token: token);

        if (achievementDal == null)
            throw new Exception("Achievement not found!");

        var userDal = await repositoryManager.Users.GetUserByIdAsync(userId, token);

        if (userDal == null)
        {
            var newUser = new UserDal { Id = userId, Achievements = new List<AchievementDal>() };
            repositoryManager.Users.CreateUser(newUser);

            newUser.Achievements.Add(achievementDal);
        }
        else
        {
            userDal.Achievements.Add(achievementDal);
        }

        await repositoryManager.SaveChangesAsync(token);
    }

    // TODO: Добавить генерацию кастомного исключения при отсутствии Пользователя или Достижения
    public async Task RemoveAchievementFromUserAsync(Guid userId, Guid achievementId, CancellationToken token = default)
    {
        var achievementDal = await repositoryManager.Achievements
            .GetAchievementByIdAsync(achievementId, token: token);

        if (achievementDal == null)
            throw new Exception("Achievement not found!");

        var userDal = await repositoryManager.Users.GetUserByIdAsync(userId, token);

        if (userDal == null)
            throw new Exception("User not found!");

        if (userDal.Achievements.Count == 0)
            return;

        userDal.Achievements.Remove(achievementDal);
        await repositoryManager.SaveChangesAsync(token);
    }
}