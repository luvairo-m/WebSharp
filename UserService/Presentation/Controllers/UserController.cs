using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Request;
using Services.Abstractions;
using Shared.Models;

namespace Presentation.Controllers;

[ApiController]
[Route("api/v1/users")]
[Produces("application/json", "application/xml")]
public class UserController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly IUserService userService;

    public UserController(IUserService userService, IMapper mapper)
    {
        this.userService = userService;
        this.mapper = mapper;
    }

    [HttpGet("{userId:guid}", Name = nameof(GetUserInfo))]
    [ProducesResponseType(typeof(IEnumerable<UserDto>), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetUserInfo([FromRoute] Guid userId)
    {
        return Ok(await userService.GetUserByIdAsync(userId));
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        var guid = await userService.CreateUserAsync(mapper.Map<UserDto>(request));
        return CreatedAtAction(nameof(GetUserInfo), new { userId = guid }, guid);
    }

    [HttpPut("{userId:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateUser([FromRoute] Guid userId, [FromBody] CreateUserRequest request)
    {
        var transformed = mapper.Map<UserDto>(request) with { Id = userId };
        await userService.UpdateUserAsync(transformed);
        return NoContent();
    }

    [HttpDelete("{userId:guid}")]
    [ProducesResponseType(404)]
    [ProducesResponseType(204)]
    public async Task<IActionResult> DeleteUser([FromRoute] Guid userId)
    {
        await userService.DeleteUserAsync(userId);
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