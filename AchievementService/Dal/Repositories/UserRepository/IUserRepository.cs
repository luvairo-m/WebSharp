using Dal.Entities;
using Dal.Repositories.BaseRepository;

namespace Dal.Repositories.UserRepository;

public interface IUserRepository : IRepositoryBase<UserDal>
{
    Task<UserDal?> GetUserByIdAsync(Guid userId, CancellationToken token = default);
    void CreateUser(UserDal user);
    void UpdateUser(UserDal user);
    void DeleteUser(UserDal user);
}