using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Request;
using Services.Abstractions;
using Shared.Models;

namespace Presentation.Controllers;

[ApiController]
[Route("api/v1/users")]
[Produces("application/json", "application/xml")]
public class ExperienceController : ControllerBase
{
    private readonly IExperienceService experienceService;
    private readonly IMapper mapper;

    public ExperienceController(IExperienceService experienceService, IMapper mapper)
    {
        this.experienceService = experienceService;
        this.mapper = mapper;
    }

    [HttpPost("{userId:guid}/experience")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> AddPointToUser(
        [FromRoute] Guid userId,
        [FromBody] UserExperienceRequest request)
    {
        await experienceService.GiveExperienceToUserAsync(userId, mapper.Map<ExperienceDto>(request));
        return Ok();
    }
}