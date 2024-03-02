using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Dal.Repositories.RepositoryBase;

public abstract class BaseRepository<TContext, TDal> : IRepositoryBase<TDal>
    where TContext : DbContext
    where TDal : class
{
    protected readonly TContext databaseContext;

    protected BaseRepository(TContext databaseContext)
    {
        this.databaseContext = databaseContext;
    }

    public IQueryable<TDal> GetAll()
    {
        return databaseContext.Set<TDal>().AsNoTracking();
    }

    public IQueryable<TDal> GetByCondition(Expression<Func<TDal, bool>> predicate)
    {
        return databaseContext.Set<TDal>().Where(predicate);
    }

    public void Create(TDal entity)
    {
        databaseContext.Set<TDal>().Add(entity);
    }

    public void Update(TDal entity)
    {
        databaseContext.Set<TDal>().Update(entity);
    }

    public void Delete(TDal entity)
    {
        databaseContext.Set<TDal>().Remove(entity);
    }
}