using Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dal.Repositories.AchievementRepository;

public class AchievementRepository : IAchievementRepository
{
    private readonly AchievementContext context;

    public AchievementRepository(AchievementContext databaseContext)
    {
        context = databaseContext;
    }

    public async Task<AchievementDal?> GetAsync(Guid guid)
    {
        return await context.Achievements.FindAsync(guid);
    }

    public async Task<Guid> CreateAsync(AchievementDal entity)
    {
        var generated = entity with { Id = Guid.NewGuid() };

        await context.Achievements.AddAsync(generated);
        await context.SaveChangesAsync();

        return generated.Id;
    }

    public async Task<bool> UpdateAsync(AchievementDal entity)
    {
        var updatedEntity = context.Update(entity);
        await context.SaveChangesAsync();

        return updatedEntity.State == EntityState.Modified;
    }

    public async Task<Guid> DeleteAsync(AchievementDal entity)
    {
        context.Remove(entity);
        await context.SaveChangesAsync();

        return entity.Id;
    }
}