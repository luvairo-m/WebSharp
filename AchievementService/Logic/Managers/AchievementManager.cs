using AutoMapper;
using Dal.Entities;
using Dal.Repositories;
using Logic.Entities;

namespace Logic.Managers;

public class AchievementManager : IAchievementManager
{
    private readonly IMapper mapper;
    private readonly IAchievementRepository repository;

    public AchievementManager(IAchievementRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<Guid> CreateAchievementAsync(AchievementDto achievementDto)
    {
        var achievementDal = mapper.Map<AchievementDal>(achievementDto) with { Id = Guid.NewGuid() };
        var inserted = await repository.CreateAchievementAsync(achievementDal);

        return inserted ? achievementDal.Id : Guid.Empty;
    }

    public async Task<Guid> DeleteAchievementAsync(Guid achievementGuid)
    {
        var deleted = await repository.DeleteAchievementAsync(achievementGuid);
        return deleted ? achievementGuid : Guid.Empty;
    }

    public async Task<AchievementDto?> GetAchievementAsync(Guid achievementGuid)
    {
        var achievementDal = await repository.GetAchievementAsync(achievementGuid);
        return mapper.Map<AchievementDto>(achievementDal);
    }

    public async Task<List<AchievementDto>?> GetUserAchievementsAsync(Guid userGuid)
    {
        var userDal = await repository.GetUserAsync(userGuid);
        return userDal?.Achievements.Select(mapper.Map<AchievementDto>).ToList();
    }

    public async Task<bool> AddAchievementToUserAsync(Guid userGuid, Guid achievementGuid)
    {
        return await repository.AddAchievementToUserAsync(userGuid, achievementGuid);
    }

    public async Task<bool> RemoveAchievementFromUserAsync(Guid userGuid, Guid achievementGuid)
    {
        return await repository.RemoveAchievementFromUserAsync(userGuid, achievementGuid);
    }

    public async Task<List<AchievementDto>> GetAllAchievements()
    {
        var achievements = await repository.GetAllAchievements();
        return achievements.Select(mapper.Map<AchievementDto>).ToList();
    }
}