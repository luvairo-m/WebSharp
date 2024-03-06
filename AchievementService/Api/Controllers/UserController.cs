using Logic.Entities;
using Logic.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/users")]
[Produces("application/json", "application/xml")]
public class UserController : ControllerBase
{
    private readonly IUserService userService;

    public UserController(IUserService userService)
    {
        this.userService = userService;
    }

    [HttpGet("{userGuid:guid}/achievements")]
    [ProducesResponseType(typeof(ICollection<AchievementDto>), 200)]
    public async Task<IActionResult> GetUsersAchievements(Guid userGuid)
    {
        return Ok(await userService.GetUserAchievementsAsync(userGuid));
    }

    [HttpPost("{userGuid:guid}/achievements/{achievementGuid:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> AddAchievementToUser(Guid userGuid, Guid achievementGuid)
    {
        await userService.AddAchievementToUserAsync(userGuid, achievementGuid);
        return Ok();
    }

    [HttpDelete("{userGuid:guid}/achievements/{achievementGuid:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> RemoveAchievementFromUser(Guid userGuid, Guid achievementGuid)
    {
        await userService.RemoveAchievementFromUserAsync(userGuid, achievementGuid);
        return Ok();
    }

    [HttpOptions]
    public IActionResult GetAllowedMethods()
    {
        var supportedMethods = $"{HttpMethods.Get}, {HttpMethods.Post}, {HttpMethods.Delete}";
        Response.Headers.Add("Allow", supportedMethods);
        return Ok();
    }
}