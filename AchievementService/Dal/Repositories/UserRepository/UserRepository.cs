using Dal.Entities;
using Dal.Repositories.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace Dal.Repositories.UserRepository;

public class UserRepository : RepositoryBase<UserDal>, IUserRepository
{
    public UserRepository(AchievementContext achievementContext) : base(achievementContext)
    {
    }

    public async Task<UserDal?> GetUserByIdAsync(Guid userId, CancellationToken token = default)
    {
        return await achievementContext.Users
            .Include(user => user.Achievements)
            .FirstOrDefaultAsync(user => user.Id == userId, token);
    }

    public void CreateUser(UserDal user)
    {
        Create(user);
    }

    public void UpdateUser(UserDal user)
    {
        Update(user);
    }

    public void DeleteUser(UserDal user)
    {
        Delete(user);
    }
}