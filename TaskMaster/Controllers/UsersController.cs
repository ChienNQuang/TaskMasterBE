using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TaskMaster.Controllers.Payloads.Requests;
using TaskMaster.Controllers.Payloads.Responses;
using TaskMaster.Models.Dtos;
using TaskMaster.Services;

namespace TaskMaster.Controllers;

[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet(Name = "GetUsers")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
    {
        var users = await _userService.GetUsers();
        return Ok(ApiResponse<IEnumerable<UserDto>>.Succeed(users));
    }

    [HttpGet("{id:guid}", Name = "GetUserById")]
    public async Task<ActionResult<UserDto>> GetUserById(Guid id)
    {
        var user = await _userService.GetUserById(id);
        return Ok(ApiResponse<UserDto>.Succeed(user));
    }

    [HttpPost(Name = "CreateUser")]
    public async Task<ActionResult<UserDto>> CreateUser([FromBody] UserCreateRequest request)
    {
        var addedUser = await _userService.AddUser(request);
        return CreatedAtRoute("GetUserById", new { addedUser.Id }, 
            ApiResponse<UserDto>.Succeed(addedUser));
    }

    [HttpPut("{id:guid}", Name = "UpdateUser")]
    public async Task<ActionResult<UserDto>> UpdateUser(Guid id, [FromBody] UserUpdateRequest request)
    {
        var updatedUser = await _userService.UpdateUser(id, request);
        return Ok(ApiResponse<UserDto>.Succeed(updatedUser));
    }

    [HttpPost("{id:guid}", Name = "DeactivateUser")]
    public async Task<ActionResult<UserDto>> DeactivateUser(Guid id)
    {
        var deactivatedUser = await _userService.DeactivateUser(id);
        return Ok(ApiResponse<UserDto>.Succeed(deactivatedUser));
    }
}