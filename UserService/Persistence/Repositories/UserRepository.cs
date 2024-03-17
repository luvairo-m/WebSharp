using Domain.Models;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly UserContext userContext;

    public UserRepository(UserContext userContext)
    {
        this.userContext = userContext;
    }

    public async Task<UserDal?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await userContext.Users
            .Include(user => user.Info)
            .FirstOrDefaultAsync(user => user.Id == userId, cancellationToken);
    }

    public async Task<UserDal?> GetUserByIdWithoutTrackingAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await userContext.Users
            .AsNoTracking()
            .Include(user => user.Info)
            .FirstOrDefaultAsync(user => user.Id == userId, cancellationToken);
    }

    public void CreateUser(UserDal user)
    {
        userContext.Users.Add(user);
    }

    public void UpdateUser(UserDal user)
    {
        userContext.Users.Update(user);
    }

    public void DeleteUser(UserDal user)
    {
        userContext.Users.Remove(user);
    }
}