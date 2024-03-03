using Api.Models.Response;
using Logic.Entities;
using Logic.Managers;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/v1/users")]
[Produces("application/json", "application/xml")]
public class UserController : ControllerBase
{
    private readonly IAchievementManager manager;

    public UserController(IAchievementManager manager)
    {
        this.manager = manager;
    }

    [HttpGet("{userGuid:guid}/achievements")]
    [ProducesResponseType(typeof(ICollection<AchievementDto>), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 404)]
    public async Task<IActionResult> GetUsersAchievements(Guid userGuid)
    {
        var achievements = await manager.GetUserAchievementsAsync(userGuid);

        if (achievements != null)
            return Ok(achievements);

        return NotFound(new ErrorResponse($"User with {userGuid} Id not found!"));
    }

    [HttpPost("{userGuid:guid}/achievements/{achievementGuid:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ErrorResponse), 404)]
    public async Task<IActionResult> AddAchievementToUser(Guid userGuid, Guid achievementGuid)
    {
        return await manager.AddAchievementToUserAsync(userGuid, achievementGuid)
            ? NoContent()
            : NotFound(new ErrorResponse($"Achievement with {achievementGuid} Id not found!"));
    }

    [HttpDelete("{userGuid:guid}/achievements/{achievementGuid:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ErrorResponse), 404)]
    public async Task<IActionResult> RemoveAchievementFromUser(Guid userGuid, Guid achievementGuid)
    {
        return await manager.RemoveAchievementFromUserAsync(userGuid, achievementGuid)
            ? NoContent()
            : NotFound(new ErrorResponse($"User with {userGuid} Id not found!"));
    }

    [HttpOptions]
    public IActionResult GetAllowedMethods()
    {
        Response.Headers.Add("Allow", "GET, POST, DELETE");
        return Ok();
    }
}