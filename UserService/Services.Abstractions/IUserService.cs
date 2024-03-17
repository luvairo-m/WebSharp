using Shared.Models;

namespace Services.Abstractions;

public interface IUserService
{
    Task<UserDto> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<Guid> CreateUserAsync(UserDto userDto, CancellationToken cancellationToken = default);
    Task UpdateUserAsync(UserDto userDto, CancellationToken cancellationToken = default);
    Task DeleteUserAsync(Guid userId, CancellationToken cancellationToken = default);
}