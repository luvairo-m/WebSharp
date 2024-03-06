using Api.Models.Request;
using AutoMapper;
using Logic.Entities;
using Logic.Services.AchievementService;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/achievements")]
[Produces("application/json", "application/xml")]
public class AchievementController : ControllerBase
{
    private readonly IAchievementService achievementService;
    private readonly IMapper mapper;

    public AchievementController(IAchievementService achievementService, IMapper mapper)
    {
        this.achievementService = achievementService;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AchievementDto>), 200)]
    public async Task<IActionResult> GetAllAchievements()
    {
        return Ok(await achievementService.GetAllAchievementsAsync());
    }

    [HttpGet("{achievementId:guid}", Name = nameof(GetAchievement))]
    [ProducesResponseType(typeof(AchievementDto), 200)]
    public async Task<IActionResult> GetAchievement([FromRoute] Guid achievementId)
    {
        return Ok(await achievementService.GetAchievementByIdAsync(achievementId));
    }

    [HttpPut("{achievementId:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> UpdateAchievement(
        [FromRoute] Guid achievementId,
        [FromBody] CreateUpdateAchievementRequest request)
    {
        var achievementDto = mapper.Map<AchievementDto>(request) with { Id = achievementId };
        await achievementService.UpdateAchievementAsync(achievementDto);
        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateAchievement([FromBody] CreateUpdateAchievementRequest request)
    {
        var guid = await achievementService.CreateAchievementAsync(mapper.Map<AchievementDto>(request));
        return CreatedAtAction(nameof(GetAchievement), new { achievementId = guid }, guid);
    }

    [HttpDelete("{achievementId:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> DeleteAchievement([FromRoute] Guid achievementId)
    {
        await achievementService.DeleteAchievementAsync(achievementId);
        return NoContent();
    }

    [HttpOptions]
    public IActionResult GetAllowedMethods()
    {
        var supportedMethods = $"{HttpMethods.Get}, {HttpMethods.Post}, {HttpMethods.Put}, {HttpMethods.Delete}";
        Response.Headers.Add("Allow", supportedMethods);
        return Ok();
    }
}