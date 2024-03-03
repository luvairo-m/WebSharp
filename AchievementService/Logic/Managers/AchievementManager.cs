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
        return await repository.CreateAchievementAsync(mapper.Map<AchievementDal>(achievementDto));
    }

    public async Task<Guid> DeleteAchievementAsync(Guid achievementGuid)
    {
        return await repository.DeleteAchievementAsync(achievementGuid);
    }

    public async Task<AchievementDto?> GetAchievementAsync(Guid achievementGuid)
    {
        var achievementDal = await repository.GetAchievementAsync(achievementGuid);
        return mapper.Map<AchievementDto>(achievementDal);
    }
}