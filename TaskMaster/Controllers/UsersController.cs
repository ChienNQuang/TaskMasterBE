using Microsoft.AspNetCore.Mvc;
using TaskMaster.Controllers.Payloads.Requests;
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

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var result = await _userService.GetUsers();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var result = await _userService.GetUserById(id);
        if (result is null)
        {
            return NotFound();
        }
        
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UserCreateRequest user)
    {
        var result = await _userService.AddUser(user);
        if (result is null)
        {
            return BadRequest();
        }
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserUpdateRequest user)
    {
        if (!id.Equals(user.Id))
        {
            return BadRequest();
        }
        var result = await _userService.UpdateUser(user);
        if (result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> DeactivateUser(Guid id)
    {
        var result = await _userService.DeactivateUser(id);
        if (result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }
}