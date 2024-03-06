using AutoMapper;
using Dal.Entities;
using Dal.RepositoryManagers;
using Logic.Entities;
using Logic.Helpers;

namespace Logic.Services.AchievementService;

internal class AchievementService : IAchievementService
{
    private readonly IMapper mapper;
    private readonly IRepositoryManager repositoryManager;

    public AchievementService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        this.repositoryManager = repositoryManager;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<AchievementDto>> GetAllAchievementsAsync(CancellationToken token = default)
    {
        return (await repositoryManager.Achievements.GetAllAchievementsAsync(token))
            .Select(mapper.Map<AchievementDto>);
    }

    public async Task<AchievementDto?> GetAchievementByIdAsync(Guid achievementId, CancellationToken token = default)
    {
        var achievementDal = await repositoryManager.Achievements
            .GetAchievementByIdAsync(achievementId, token: token);
        ExceptionHelper.RaiseNotFoundWhenNull(achievementDal, achievementId);

        return mapper.Map<AchievementDto>(achievementDal);
    }

    public async Task<Guid> CreateAchievementAsync(AchievementDto achievementDto, CancellationToken token = default)
    {
        var achievementDal = mapper.Map<AchievementDal>(achievementDto);
        repositoryManager.Achievements.CreateAchievement(achievementDal);

        await repositoryManager.SaveChangesAsync(token);

        return achievementDal.Id;
    }

    public async Task UpdateAchievementAsync(AchievementDto achievementDto, CancellationToken token = default)
    {
        var achievementDal = mapper.Map<AchievementDal>(achievementDto);
        var achievementFound = await repositoryManager.Achievements
            .GetAchievementByIdAsync(achievementDto.Id, false, token);
        ExceptionHelper.RaiseNotFoundWhenNull(achievementFound, achievementDto.Id);

        repositoryManager.Achievements.UpdateAchievement(achievementDal);
        await repositoryManager.SaveChangesAsync(token);
    }

    public async Task DeleteAchievementAsync(Guid achievementId, CancellationToken token = default)
    {
        var achievementDal = await repositoryManager.Achievements
            .GetAchievementByIdAsync(achievementId, token: token);
        ExceptionHelper.RaiseNotFoundWhenNull(achievementDal, achievementId);

        repositoryManager.Achievements.DeleteAchievement(achievementDal!);
        await repositoryManager.SaveChangesAsync(token);
    }
}