using AutoMapper;
using Domain.Exceptions;
using Domain.Models;
using Domain.Repository;
using Services.Abstractions;
using Shared.Models;

namespace Services;

public class UserService : IUserService
{
    private readonly IMapper mapper;
    private readonly IRepositoryManager repositoryManager;

    public UserService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        this.repositoryManager = repositoryManager;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync(CancellationToken cancellationToken = default)
    {
        var users = await repositoryManager.Users.GetAllUsersAsync(cancellationToken);
        return users.Select(mapper.Map<UserDto>);
    }

    public async Task<UserDto> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await repositoryManager.Users.GetUserByIdAsync(userId, cancellationToken)
                   ?? throw new UserNotFoundException(userId);

        return mapper.Map<UserDto>(user);
    }

    public async Task CreateUserAsync(UserDto userDto, CancellationToken cancellationToken = default)
    {
        repositoryManager.Users.CreateUser(mapper.Map<UserDal>(userDto));
        await repositoryManager.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateUserAsync(UserDto userDto, CancellationToken cancellationToken = default)
    {
        if (!await repositoryManager.Users.UserExistsAsync(userDto.Id, cancellationToken))
            throw new UserNotFoundException(userDto.Id);

        repositoryManager.Users.UpdateUser(mapper.Map<UserDal>(userDto));
        await repositoryManager.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        if (!await repositoryManager.Users.UserExistsAsync(userId, cancellationToken))
            throw new UserNotFoundException(userId);

        var userDto = await GetUserByIdAsync(userId, cancellationToken);
        repositoryManager.Users.DeleteUser(mapper.Map<UserDal>(userDto));

        await repositoryManager.SaveChangesAsync(cancellationToken);
    }
}