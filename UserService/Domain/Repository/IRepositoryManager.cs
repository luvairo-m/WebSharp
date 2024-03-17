namespace Domain.Repository;

public interface IRepositoryManager
{
    IUserRepository Users { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}