using Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dal.Repositories.UserRepository;

public class UserRepository : IUserRepository
{
    private readonly AchievementContext context;

    public UserRepository(AchievementContext databaseContext)
    {
        context = databaseContext;
    }

    public async Task<UserDal?> GetAsync(Guid guid)
    {
        return await context.Users.FindAsync(guid);
    }

    public async Task<Guid> CreateAsync(UserDal entity)
    {
        var generated = entity with { Id = Guid.NewGuid() };

        await context.Users.AddAsync(generated);
        await context.SaveChangesAsync();

        return generated.Id;
    }

    public async Task<bool> UpdateAsync(UserDal entity)
    {
        var updatedEntity = context.Update(entity);
        await context.SaveChangesAsync();

        return updatedEntity.State == EntityState.Modified;
    }

    public async Task<Guid> DeleteAsync(UserDal entity)
    {
        context.Remove(entity);
        await context.SaveChangesAsync();

        return entity.Id;
    }
}