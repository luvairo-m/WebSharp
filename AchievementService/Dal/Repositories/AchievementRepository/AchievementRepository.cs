using Dal.Entities;
using Dal.Repositories.RepositoryBase;

namespace Dal.Repositories.AchievementRepository;

public class AchievementRepository : BaseRepository<AchievementContext, AchievementDal>, IAchievementRepository
{
    public AchievementRepository(AchievementContext databaseContext) : base(databaseContext)
    {
    }
}