using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Dal.Repositories.BaseRepository;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected RepositoryBase(AchievementContext achievementContext)
    {
        this.achievementContext = achievementContext;
    }

    protected AchievementContext achievementContext { get; set; }

    public IQueryable<T> FindAll()
    {
        return achievementContext.Set<T>().AsNoTracking();
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
    {
        return achievementContext.Set<T>().Where(expression).AsNoTracking();
    }

    public void Create(T entity)
    {
        achievementContext.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        achievementContext.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        achievementContext.Set<T>().Remove(entity);
    }
}