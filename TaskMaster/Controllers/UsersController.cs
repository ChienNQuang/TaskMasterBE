using Microsoft.AspNetCore.Mvc;
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
    
    
}