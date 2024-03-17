using AchievementServiceConnectionLib.Contracts;
using AutoMapper;
using Domain.Exceptions;
using Domain.Models;
using Domain.Repository;
using Services.Abstractions;
using Shared.Models;

namespace Services;

internal class UserService : IUserService
{
    private readonly IAchievementConnectionService connectionService;
    private readonly IMapper mapper;
    private readonly IRepositoryManager repositoryManager;

    public UserService(
        IRepositoryManager repositoryManager,
        IAchievementConnectionService connectionService,
        IMapper mapper)
    {
        this.repositoryManager = repositoryManager;
        this.connectionService = connectionService;
        this.mapper = mapper;
    }

    public async Task<UserDto> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await repositoryManager.Users.GetUserByIdAsync(userId, cancellationToken)
                   ?? throw new UserNotFoundException(userId);

        var userDto = mapper.Map<UserDto>(user);

        var userAchievements = await connectionService
            .GetUserAchievementsAsync(userId, cancellationToken);

        foreach (var achievement in userAchievements)
        {
            userDto.Achievements.Add(new AchievementDto
            {
                Title = achievement.Title,
                Description = achievement.Description,
                Points = achievement.Points,
                ImageUrl = achievement.ImageUrl
            });
        }

        return userDto;
    }

    public async Task<Guid> CreateUserAsync(UserDto userDto, CancellationToken cancellationToken = default)
    {
        var user = mapper.Map<UserDal>(userDto);
        repositoryManager.Users.CreateUser(user);

        await repositoryManager.SaveChangesAsync(cancellationToken);

        return user.Id;
    }

    public async Task UpdateUserAsync(UserDto userDto, CancellationToken cancellationToken = default)
    {
        var user = await repositoryManager.Users
            .GetUserByIdWithoutTrackingAsync(userDto.Id, cancellationToken);

        if (user == null)
            throw new UserNotFoundException(userDto.Id);

        var userDal = mapper.Map<UserDal>(userDto);
        userDal.Info!.Id = user.Info!.Id;

        repositoryManager.Users.UpdateUser(userDal);
        await repositoryManager.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await repositoryManager.Users.GetUserByIdWithoutTrackingAsync(userId, cancellationToken)
                   ?? throw new UserNotFoundException(userId);

        repositoryManager.Users.DeleteUser(user);
        await repositoryManager.SaveChangesAsync(cancellationToken);
    }
}