using Shared.Models;

namespace Services.Abstractions;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync(CancellationToken cancellationToken = default);
    Task<UserDto> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task CreateUserAsync(UserDto userDto, CancellationToken cancellationToken = default);
    Task UpdateUserAsync(UserDto userDto, CancellationToken cancellationToken = default);
    Task DeleteUserAsync(Guid userId, CancellationToken cancellationToken = default);
}