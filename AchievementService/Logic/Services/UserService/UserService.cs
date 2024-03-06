using AutoMapper;
using Dal.Entities;
using Dal.RepositoryManagers;
using Logic.Entities;
using Logic.Helpers;

namespace Logic.Services.UserService;

internal class UserService : IUserService
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
        ExceptionHelper.RaiseNotFoundWhenNull(userDal, userId);

        return userDal!.Achievements.Select(mapper.Map<AchievementDto>);
    }

    public async Task AddAchievementToUserAsync(Guid userId, Guid achievementId, CancellationToken token = default)
    {
        var achievementDal = await repositoryManager.Achievements
            .GetAchievementByIdAsync(achievementId, token: token);
        ExceptionHelper.RaiseNotFoundWhenNull(achievementDal, achievementId);

        var userDal = await repositoryManager.Users.GetUserByIdAsync(userId, token);

        if (userDal == null)
        {
            var newUser = new UserDal { Id = userId, Achievements = new List<AchievementDal>() };
            repositoryManager.Users.CreateUser(newUser);

            newUser.Achievements.Add(achievementDal!);
        }
        else
        {
            userDal.Achievements.Add(achievementDal!);
        }

        await repositoryManager.SaveChangesAsync(token);
    }

    public async Task RemoveAchievementFromUserAsync(Guid userId, Guid achievementId, CancellationToken token = default)
    {
        var achievementDal = await repositoryManager.Achievements
            .GetAchievementByIdAsync(achievementId, token: token);
        ExceptionHelper.RaiseNotFoundWhenNull(achievementDal, achievementId);

        var userDal = await repositoryManager.Users.GetUserByIdAsync(userId, token);
        ExceptionHelper.RaiseNotFoundWhenNull(userDal, userId);

        if (userDal!.Achievements.Count != 0)
        {
            userDal.Achievements.Remove(achievementDal!);
            await repositoryManager.SaveChangesAsync(token);
        }
    }
}