namespace Dal.Repositories.RepositoryBase;

public interface IRepositoryBase<T>
{
    Task<T?> GetAsync(Guid guid);
    Task<Guid> CreateAsync(T entity);
    Task<bool> UpdateAsync(T entity);
    Task<Guid> DeleteAsync(T entity);
}