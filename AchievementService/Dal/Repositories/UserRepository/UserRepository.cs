using Dal.Entities;
using Dal.Repositories.RepositoryBase;

namespace Dal.Repositories.UserRepository;

public class UserRepository : BaseRepository<AchievementContext, UserDal>, IUserRepository
{
    public UserRepository(AchievementContext databaseContext) : base(databaseContext)
    {
    }
}