using Domain.Models;

namespace Domain.Repository;

public interface IUserRepository
{
    Task<IEnumerable<UserDal>> GetAllUsersAsync(CancellationToken cancellationToken = default);
    Task<UserDal?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<bool> UserExistsAsync(Guid userId, CancellationToken cancellationToken = default);
    void CreateUser(UserDal user);
    void UpdateUser(UserDal user);
    void DeleteUser(UserDal user);
}