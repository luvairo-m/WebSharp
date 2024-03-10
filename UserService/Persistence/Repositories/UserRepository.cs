using Domain.Models;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserContext userContext;

    public UserRepository(UserContext userContext)
    {
        this.userContext = userContext;
    }

    public async Task<IEnumerable<UserDal>> GetAllUsersAsync(CancellationToken cancellationToken = default)
    {
        return await userContext.Users
            .AsNoTracking()
            .Include(user => user.Info)
            .ToListAsync(cancellationToken);
    }

    public async Task<UserDal?> GetUserByIdAsync(Guid userId, CancellationToken token = default)
    {
        var keyValues = new object[] { userId };
        return await userContext.Users.FindAsync(keyValues, token);
    }

    public async Task<bool> UserExistsAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await userContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Id == userId, cancellationToken) != null;
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