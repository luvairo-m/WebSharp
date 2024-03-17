using Domain.Repository;
using Persistence.Contexts;

namespace Persistence.Repositories;

internal class RepositoryManager : IRepositoryManager
{
    private readonly UserContext userContext;

    public RepositoryManager(UserContext userContext, IUserRepository userRepository)
    {
        this.userContext = userContext;
        Users = userRepository;
    }

    public IUserRepository Users { get; }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await userContext.SaveChangesAsync(cancellationToken);
    }
}