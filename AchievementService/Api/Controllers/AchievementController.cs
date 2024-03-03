using Api.Models.Request;
using Api.Models.Response;
using AutoMapper;
using Logic.Entities;
using Logic.Managers;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/v1/achievements")]
[Produces("application/json", "application/xml")]
public class AchievementController : ControllerBase
{
    private readonly IAchievementManager manager;
    private readonly IMapper mapper;

    public AchievementController(IAchievementManager manager, IMapper mapper)
    {
        this.manager = manager;
        this.mapper = mapper;
    }

    [HttpGet("{achievementGuid:guid}", Name = nameof(GetAchievement))]
    [ProducesResponseType(typeof(AchievementDto), 200)]
    [ProducesResponseType(typeof(ErrorResponse), 404)]
    public async Task<IActionResult> GetAchievement(Guid achievementGuid)
    {
        var achievement = await manager.GetAchievementAsync(achievementGuid);

        if (achievement != null)
            return Ok(achievement);

        var errorResponse = new ErrorResponse(
            "Id", $"Achievement with {achievementGuid} guid not found!");

        return NotFound(errorResponse);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateAchievement([FromBody] CreateAchievementRequest request)
    {
        var guid = await manager.CreateAchievementAsync(mapper.Map<AchievementDto>(request));

        return CreatedAtAction(
            nameof(GetAchievement),
            new { achievementGuid = guid },
            guid);
    }

    [HttpDelete("{achievementGuid:guid}")]
    [ProducesResponseType(typeof(ErrorResponse), 404)]
    [ProducesResponseType(204)]
    public async Task<IActionResult> DeleteAchievement(Guid achievementGuid)
    {
        await manager.DeleteAchievementAsync(achievementGuid);
        return NoContent();
    }

    [HttpOptions]
    public IActionResult GetAllowedActions()
    {
        Response.Headers.Add("Allow", "GET, POST, DELETE");
        return Ok();
    }
}