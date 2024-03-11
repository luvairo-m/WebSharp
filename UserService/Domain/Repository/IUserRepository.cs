using Domain.Models;

namespace Domain.Repository;

public interface IUserRepository
{
    Task<UserDal?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<UserDal?> GetUserByIdWithoutTrackingAsync(Guid userId, CancellationToken cancellationToken = default);
    void CreateUser(UserDal user);
    void UpdateUser(UserDal user);
    void DeleteUser(UserDal user);
}